"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
$(document).keypress(function (e) {
    if (e.which == 13) {
        $("#buttonSend").click();
        e.preventDefault();
    }
});
//Disable send button until connection is established
document.getElementById("buttonSend").disabled = true;
var objDiv = document.getElementById("messages");
objDiv.scrollTop = objDiv.scrollHeight;
var lastUser = document.getElementById('hiddenLastUser').value;
connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var lastSide = document.getElementById('lastSide').value;
    if (lastUser===user) {
        if (lastSide === 0) {

            let div = document.createElement('div');
            div.className = 'd-flex justify-content-start mb-4';
            let insideDiv = document.createElement('div');
            insideDiv.className = 'msg_cotainer';
            insideDiv.innerHTML = message;
            let span = document.createElement('span');
            span.className = 'msg_time';
            var date = new Date();
            var sender = document.createElement('div');
            sender.className = 'img_cont_msg';
            sender.innerHTML = user;
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
            insideDiv.innerHTML = message;
            let span = document.createElement('span');
            span.className = 'msg_time_send';
            var date = new Date();
            var sender = document.createElement('div');
            sender.className = 'img_cont_msg';
            sender.innerHTML = user;
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
    } else if (lastUser!==user) {
        if (lastSide === 0) {
            let div = document.createElement('div');
            div.className = 'd-flex justify-content-end mb-4';
            let insideDiv = document.createElement('div');
            insideDiv.className = 'msg_cotainer_send';
            insideDiv.innerHTML = message;
            let span = document.createElement('span');
            span.className = 'msg_time_send';
            var date = new Date();
            var sender = document.createElement('div');
            sender.className = 'img_cont_msg';
            sender.innerHTML = user;
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
            insideDiv.innerHTML = message;
            let span = document.createElement('span');
            span.className = 'msg_time';
            var date = new Date();
            var sender = document.createElement('div');
            sender.className = 'img_cont_msg';
            sender.innerHTML = user;
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
    constructor(content, eventId, createdBy) {
        this.content = content;
        this.eventId = eventId;
        this.createdBy = createdBy;
    }
}
document.getElementById("buttonSend").addEventListener("click", function (event) {
    let messageObj = new Message(document.getElementById("textMessage").value, document.getElementById("hiddenEventId").value, document.getElementById("hiddenName").value)
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