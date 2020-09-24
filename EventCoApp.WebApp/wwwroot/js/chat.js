"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
$(document).keypress(function (e) {
    if (e.which == 13) {
        $("#buttonSend").click();
        e.preventDefault();
    }
});
//Disable send button until connection is established
if (document.getElementById("buttonSend") != null) {

    document.getElementById("buttonSend").disabled = true;
}
if (document.getElementById("messages") != null) {

    var objDiv = document.getElementById("messages");
    objDiv.scrollTop = objDiv.scrollHeight;
}
if (document.getElementById('hiddenLastUser') != null) {

    var lastUser = document.getElementById('hiddenLastUser').value;
}
connection.on("ReceiveMessage", function (user, message) {
    var msg = message.content;
    var lastSide = parseInt(document.getElementById('lastSide').value);
    var photo = '/images/' + message.photo;
    lastUser = document.getElementById('hiddenLastUser').value;
    if (lastUser === user) {
        if (lastSide === 0) {

            let div = document.createElement('div');
            div.className = 'd-flex justify-content-start mb-4';
            let insideDiv = document.createElement('div');
            insideDiv.className = 'msg_cotainer';
            insideDiv.innerHTML = msg;
            let span = document.createElement('span');
            span.className = 'msg_time';
            var date = new Date();
            var sender = document.createElement('div');
            sender.className = 'img_cont_msg';
            var img = document.createElement("IMG");
            img.src = photo;
            img.width = 50;
            img.height = 50;
            sender.appendChild(img);
            span.innerHTML = formatDate(date);
            insideDiv.appendChild(span);

            div.appendChild(sender);
            div.appendChild(insideDiv);

            var encodedMsg = user + " says " + msg;
            var li = document.createElement("li");
            li.textContent = encodedMsg;
            document.getElementById("messages").appendChild(div);
            objDiv.scrollTop = objDiv.scrollHeight;
            document.getElementById('lastSide').value = 0;
        } else {

            let div = document.createElement('div');
            div.className = 'd-flex justify-content-end mb-4';
            let insideDiv = document.createElement('div');
            insideDiv.className = 'msg_cotainer_send';
            insideDiv.innerHTML = msg;
            let span = document.createElement('span');
            span.className = 'msg_time_send';
            var date = new Date();
            var sender = document.createElement('div');
            sender.className = 'img_cont_msg';
            var img = document.createElement("IMG");
            img.src = photo;
            img.width = 50;
            img.height = 50;
            sender.appendChild(img);
            span.innerHTML = formatDate(date);
            insideDiv.appendChild(span);
            div.appendChild(insideDiv);

            div.appendChild(sender);
            var encodedMsg = user + " says " + msg;
            var li = document.createElement("li");
            li.textContent = encodedMsg;
            document.getElementById("messages").appendChild(div);
            objDiv.scrollTop = objDiv.scrollHeight;
            document.getElementById('lastSide').value = 1;
        }
        document.getElementById('hiddenLastUser').value = user;
    } else if (lastUser !== user) {
        if (lastSide === 0) {
            let div = document.createElement('div');
            div.className = 'd-flex justify-content-end mb-4';
            let insideDiv = document.createElement('div');
            insideDiv.className = 'msg_cotainer_send';
            insideDiv.innerHTML = msg;
            let span = document.createElement('span');
            span.className = 'msg_time_send';
            var date = new Date();
            var img = document.createElement("IMG");
            img.src = photo;
            img.width = 50;
            img.height = 50;
            var sender = document.createElement('div');
            sender.className = 'img_cont_msg';
            sender.appendChild(img);
            span.innerHTML = formatDate(date);
            insideDiv.appendChild(span);
            div.appendChild(insideDiv);

            div.appendChild(sender);
            var encodedMsg = user + " says " + msg;
            var li = document.createElement("li");
            li.textContent = encodedMsg;
            document.getElementById("messages").appendChild(div);
            objDiv.scrollTop = objDiv.scrollHeight;
            document.getElementById('lastSide').value = 1;
        } else {
            let div = document.createElement('div');
            div.className = 'd-flex justify-content-start mb-4';
            let insideDiv = document.createElement('div');
            insideDiv.className = 'msg_cotainer';
            insideDiv.innerHTML = msg;
            let span = document.createElement('span');
            span.className = 'msg_time';
            var date = new Date();
            var img = document.createElement("IMG");
            img.src = photo;
            img.width = 50;
            img.height = 50;
            var sender = document.createElement('div');
            sender.className = 'img_cont_msg';
            sender.appendChild(img);
            span.innerHTML = formatDate(date);
            insideDiv.appendChild(span);

            div.appendChild(sender);
            div.appendChild(insideDiv);

            var encodedMsg = user + " says " + msg;
            var li = document.createElement("li");
            li.textContent = encodedMsg;
            document.getElementById("messages").appendChild(div);
            objDiv.scrollTop = objDiv.scrollHeight;
            document.getElementById('lastSide').value = 0;
        }
        document.getElementById('hiddenLastUser').value = user;
    }

});
function formatDate(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var seconds = date.getSeconds();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    seconds = seconds < 10 ? '0' + seconds : seconds;
    var strTime = hours + ':' + minutes + ' ' + seconds + ' ' + ampm;
    return (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + "  " + strTime;
}
connection.start().then(function () {
    document.getElementById("buttonSend").disabled = false;
}).catch(function (err) {
}).catch(function (err) {
    return console.error(err.toString());
});

class Message {
    constructor(content, eventId, createdBy, photo) {
        this.content = content;
        this.eventId = eventId;
        this.createdBy = createdBy;
        this.photo = photo;
    }
}
if (document.getElementById("buttonSend") != null) {

    document.getElementById("buttonSend").addEventListener("click", function (event) {
        let messageObj = new Message(document.getElementById("textMessage").value, document.getElementById("hiddenEventId").value, document.getElementById("hiddenName").value, document.getElementById('hiddenUserPhotoName').value)
        var json = JSON.stringify(messageObj);
        var user = document.getElementById("hiddenName").value;
        var message = document.getElementById("textMessage").value;
        if (message !== null && message !== "") {
            connection.invoke("SendMessage", user, json).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        }
        document.getElementById("textMessage").value = '';
    });
}