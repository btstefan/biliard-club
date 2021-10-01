// Javascript (JQ) for Employee page.


var reservationHub = $.connection.reservationHub;

// connect to hub
$.connection.hub.start()
    .done(function () {
        console.log("radi");
    })
    .fail(function () { alert("Connection error") })

// client hub function, reload reservations
reservationHub.client.insertReservation = function (reservation) {
    $("#reservations").load(location.href + " #reservations>*", "");
}

// ajax post request for status update
$(document).on("click", ".change-status", function () {
    var statusId = $(this).attr("status-id");
    var reservationId = $(this).parent().parent().attr("reservation-id");

    console.log(statusId + "-" + reservationId);

    $.ajax({
        type: "POST",
        url: "/Reservation/ChangeStatus",
        data: {
            reservationId: reservationId,
            statusId: statusId
        },
        error: function () {
            alert("Error");
        }
    });
});

// client hub function, change status
reservationHub.client.UpdateReservation = function (reservationId, statusName) {
    $(".reservation[reservation-id='" + reservationId + "']").children(".status-id").text(statusName);
}