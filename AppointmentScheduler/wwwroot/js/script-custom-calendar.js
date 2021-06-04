﻿var routeUrl = location.protocol + "//" + location.host;
$(document).ready(function ()
{
    $("#appointmentDate").kendoDateTimePicker(
        {
            value: new Date(),
            dateInput: false,
            format: "dd/MM/yyyy HH:mm:ss"
        });
    InitializeCalendar();
});

var calendar;
function InitializeCalendar()
{
    try
    {

        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null)
        {
            calendar = new FullCalendar.Calendar(calendarEl,
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
                    },
                    eventDisplay: 'Block',
                    events: function (fetchInfo, successCallback, failureCallback)
                    {
                        $.ajax(
                            {
                                url: routeUrl + '/api/Appointment/GetCalendarData?hairdresserId=' + $("#hairdresserId").val(),
                                type: 'GET',
                                dataType: 'JSON',
                                success: function (response)
                                {
                                    var events = [];

                                    if (response.status === 1 ) {
                                        $.each(response.data_Enum, function (i, data) {
                                            events.push({
                                                title: data.title,
                                                description: data.desctiption,
                                                start: data.startDate,
                                                end: data.endDate,
                                                backgroundColor: data.isHairdresserApproved ? "#28a745" : "#dc3545",
                                                textColor: "white",
                                                borderColor: "#162466",
                                                id: data.id
                                            });
                                        })
                                    }
                                    successCallback(events);

                                },
                                error: function (xhr) {
                                    $.notify("Error", "error");

                                }
                            });
                    },
                    eventClick: function (info)
                    {
                        getEventDetailsById(info.event);
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
    if (isEventDetails != null) {

        $("#title").val(obj.title);
        $("#description").val(obj.desctiption);
        $("#appointmentDate").val(obj.startDate);
        $("#duration").val(obj.duration);
        $("#hairdresserId").val(obj.hairdresserId);
        $("#clientId").val(obj.clientId);
        $("#id").val(obj.id);
        $("#lblHairdresserName").html(obj.hairdresserName);
        $("#lblClientName").html(obj.clientName);

        if (obj.isHairdresserApproved) {
            $("#lblStatus").html('Approved');
            $("#btn_confirm").addClass('d-none');
            $("#btn_submit").addClass('d-none');
        }
        else
        {
            $("#lblStatus").html('Pending');
        }
    }
    else {
        $("#appointmentDate").val(obj.startStr + " " + new moment().format("hh:mm A"));
        $("#id").val(0);
    }
    $("#appointmentInput").modal("show");
}

function onCloseModal(obj, isEventDetails)
{
    $("#appointmentForm")[0].reset();
    $("#id").val(0);
    $("#title").val('');
    $("#description").val('');
    $("#appointmentDate").val('');
    $("#duration").val('');
    $("#clientId").val('');
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
                        calendar.refetchEvents();
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

function getEventDetailsById(info)
{
    $.ajax(
        {
            url: routeUrl + '/api/Appointment/GetCalendarDataById/' + info.id,
            type: 'GET',
            dataType: 'JSON',
            success: function (response) {
                if (response.status === 1 && response.data_Enum != undefined)
                {
                    onShowModal(response.data_Enum, true);
                }

            },
            error: function (xhr) {
                $.notify("Error", "error");

            }
        })
}

function onHairdresserChange()
{
    calendar.refetchEvents();
}

function onDeleteAppointment()
{
    var id = parseInt($("#id").val());

    $.ajax(
        {
            url: routeUrl + '/api/Appointment/DeleteAppointment/' + id,
            type: 'GET',
            dataType: 'JSON',
            success: function (response) {
                if (response.status === 1) {
                    $.notify(response.message, "success");
                    calendar.refetchEvents();
                    onCloseModal();
                }
                else
                {
                    $.notify(response.message, "error");
                }

            },
            error: function (xhr) {
                $.notify("Error", "error");

            }
        })
}

function onConfirmAppointment()
{
    var id = parseInt($("#id").val());

    $.ajax(
        {
            url: routeUrl + '/api/Appointment/ConfirmAppointment/' + id,
            type: 'GET',
            dataType: 'JSON',
            success: function (response) {
                if (response.status === 1) {
                    $.notify(response.message, "success");
                    calendar.refetchEvents();
                    onCloseModal();
                }
                else
                {
                    $.notify(response.message, "error");
                }

            },
            error: function (xhr) {
                $.notify("Error", "error");

            }
        })
}