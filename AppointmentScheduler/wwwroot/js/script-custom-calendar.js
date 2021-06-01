var routeUrl = location.protocol + "//" + location.host;
$(document).ready(function ()
{
    $("#appointmentDate").kendoDateTimePicker(
        {
            value: new Date(),
            dateInput: false
        });
    InitializeCalendar();
});


function InitializeCalendar()
{
    try
    {

        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null)
        {
            var calendar = new FullCalendar.Calendar(calendarEl,
                {
                    initialView: 'dayGridMonth',
                    headerToolbar:
                    {
                        left: 'prev,next,today',
                        center: 'title',
                        right: 'dayGridMonth,timeGridWeek,timeGridDay'
                    },
                    selectable: true,
                    editable: false,
                    select: function (event) {
                        onShowModal(event, null);
                    }

                });

            calendar.render();

        }      
    }
    catch (e)
    {
        alert(e);
    }
}

function onShowModal(obj, isEventDetails)
{
    $("#appointmentInput").modal("show");
}

function onCloseModal(obj, isEventDetails)
{
    $("#appointmentInput").modal("hide");
}

function onSubmitForm()
{
    if (CheckValidations()) {


        var requestData =
        {
            Id: parseInt($("#id").val()),
            Title: $("#title").val(),
            Desctiption: $("#description").val(),
            StartDate: $("#appointmentDate").val(),
            Duration: $("#duration").val(),
            HairdresserId: $("#hairdresserId").val(),
            ClientId: $("#clientId").val()
        }

        $.ajax(
            {
                url: routeUrl + '/api/Appointment/SaveCalendarData',
                type: 'POST',
                data: JSON.stringify(requestData),
                contentType: 'application/json',
                success: function (response) {
                    if (response.status === 1 || response.status === 2) {
                        $.notify(response.message, "success");
                        onCloseModal();
                    }
                    else {
                        $.notify(response.message, "error");
                    }
                },
                error: function (xhr) {
                    $.notify("Error", "error");

                }
            });
    }
}

function CheckValidations()
{
    var isValid = true; 

    if ($("#title").val() === undefined || $("#title").val() === "") {
        isValid = false;
        $("#title").addClass("error");
    }
    else
    {
        $("#title").removeClass("error");
    }

    if ($("#appointmentDate").val() === undefined || $("#appointmentDate").val() === "") {
        isValid = false;
        $("#appointmentDate").addClass("error");
    }
    else {
        $("#appointmentDate").removeClass("error");
    }

    return isValid;
}