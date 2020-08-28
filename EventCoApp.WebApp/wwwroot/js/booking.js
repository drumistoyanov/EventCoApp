$('#TicketCount').change(function () {
    var value = $('#TicketCount').val() * $('#TicketPrice').val()
    $('#WholePrice').val(value.toFixed(2));
});