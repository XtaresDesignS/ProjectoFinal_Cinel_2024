<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/BackendPages/BackendLayout.Master" AutoEventWireup="true" CodeBehind="Calendario_Opt_TS.aspx.cs" Inherits="ProjectoFinal_Cinel_2024.Pages.BackendPages.Pages.Calendario_Opt_TS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.dhtmlx.com/scheduler/edge/dhtmlxscheduler.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.dhtmlx.com/scheduler/edge/dhtmlxscheduler.js"></script>
    <script src="https://cdn.dhtmlx.com/scheduler/locale/locale_pt.js"></script>
    <!-- Adiciona o pacote de localização em português -->

</asp:Content>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBackEnd" runat="server">
    <div class="main-content col">
        <div id="scheduler_here" class="dhx_cal_container" style='width:90dvw; height:80dvh;'>
            <div class="dhx_cal_navline">
                <div class="dhx_cal_prev_button"> </div>
                <div class="dhx_cal_next_button"> </div>
                <div class="dhx_cal_today_button"></div>
                <div class="dhx_cal_date"></div>
                <div class="dhx_cal_tab" name="day_tab"></div>
                <div class="dhx_cal_tab" name="week_tab"></div>
                <div class="dhx_cal_tab" name="month_tab"></div>
            </div>
            <div class="dhx_cal_header"></div>
            <div class="dhx_cal_data"></div>
        </div>
        <button id="saveButton" onclick="saveData()">Salvar</button>
        <script>
            scheduler.config.drag_resize = false;
            scheduler.config.drag_move = false;
            scheduler.config.drag_create = false;
            scheduler.config.first_hour = 8;
            scheduler.config.last_hour = 24;

            scheduler.ignore_timeline = function (date) {
                // Ignora domingos e feriados
                if (date.getDay() === 0 || isHoliday(date)) {
                    return true;
                }
                return false;
            };

            scheduler.attachEvent("onClick", function (id, e) {
                // Código para lidar com o clique do usuário
                // Você pode usar AJAX para enviar os dados do evento para um script do lado do servidor que irá salvá-lo no banco de dados
            });

            scheduler.init('scheduler_here', new Date(), "week");

            function saveData() {
                var events = scheduler.getEvents();
                var userId = "yourUserId"; // Substitua por seu ID de usuário

                // Aqui você pode usar AJAX para enviar os dados para o servidor
                // Por exemplo:
                $.ajax({
                    url: 'apiUrl', // Substitua por seu URL da API
                    type: 'POST',
                    data: {
                        userId: userId,
                        events: events
                    },
                    success: function (response) {
                        // Lidar com a resposta do servidor
                    }
                });
            }
        </script>
    </div>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBackEnd" runat="server">
     <div class="main-content col">
    <div id="scheduler_here" class="dhx_cal_container" style='width: 90dvw; height: 80dvh;'>
        <div class="dhx_cal_navline">
            <div class="dhx_cal_prev_button"> </div>
            <div class="dhx_cal_next_button"> </div>
            <div class="dhx_cal_today_button"></div>
            <div class="dhx_cal_date"></div>
            <div class="dhx_cal_tab" name="day_tab"></div>
            <div class="dhx_cal_tab" name="week_tab"></div>
            <div class="dhx_cal_tab" name="month_tab"></div>
            <div class="dhx_cal_tab" name="year_tab"></div>
            <button id="saveButton" onclick="event.preventDefault(); saveData();">Salvar</button>
            <button id="markMorningButton" onclick="event.preventDefault(); markMorning();">Marcar Manhã</button>
            <button id="markAfternoonButton" onclick="event.preventDefault(); markAfternoon();">Marcar Tarde</button>
            <button id="markNightButton" onclick="event.preventDefault(); markNight();">Marcar Noite</button>
            <button id="clearButton" onclick="event.preventDefault(); clearAll();">Limpar Tudo</button>
        </div>
        <div class="dhx_cal_header"></div>
        <div class="dhx_cal_data"></div>
    </div>
</div>

<link rel="stylesheet" href="https://cdn.dhtmlx.com/scheduler/edge/dhtmlxscheduler_material.css">

<script src="https://cdn.dhtmlx.com/scheduler/edge/dhtmlxscheduler.js"></script>
<script src="https://cdn.dhtmlx.com/scheduler/locale/locale_pt.js"></script>
<script src="https://cdn.dhtmlx.com/scheduler/edge/ext/dhtmlxscheduler_year_view.js"></script>

<script>
    scheduler.config.drag_resize = false;
    scheduler.config.drag_move = false;
    scheduler.config.drag_create = false;
    scheduler.config.first_hour = 8;
    scheduler.config.last_hour = 24;
    scheduler.config.time_step = 60;
    scheduler.ignore_timeline = function (date) {
        if (date.getDay() === 0 || isHoliday(date) || isUnpaidLeave(date)) {
            return true;
        }
        return false;
    };
    scheduler.attachEvent("onBeforeEventChanged", function (event_object, native_event, is_new, original) {
        if (event_object.start_date.getDay() === 0 || event_object.end_date.getDay() === 0 || isUnpaidLeave(event_object.start_date) || isUnpaidLeave(event_object.end_date) || isPastHour(event_object.start_date)) {
            return false;
        }
        return true;
    });
    scheduler.attachEvent("onClick", function (id, e) {
        // Código para lidar com o clique do usuário
        scheduler.select(id);
        return true;
    });
    scheduler.init('scheduler_here', new Date(), "week");
    document.getElementById('saveButton').addEventListener('click', function (e) {
        e.preventDefault();
        var events = scheduler.getEvents();
        console.log(events);
    });
    document.getElementById('clearButton').addEventListener('click', function (e) {
        e.preventDefault();
        scheduler.clearAll();
    });
    document.getElementById('deleteSelectedButton').addEventListener('click', function (e) {
        e.preventDefault();
        var selectedEventId = scheduler.getState().selected_event;
        if (selectedEventId) {
            scheduler.deleteEvent(selectedEventId);
        }
    });
    function markMorning() {
        markTimePeriod(8, 10, "Período da manhã");
    }
    function markAfternoon() {
        markTimePeriod(14, 18, "Período da tarde");
    }
    function markNight() {
        markTimePeriod(18, 24, "Período da noite");
    }
    function markTimePeriod(startHour, endHour, text) {
        var view = scheduler.getState().mode;
        var date = scheduler.getState().date;

        if (view === "day" || view === "week" || view === "month" || view === "year") {
            var startDay = view === "day" ? date.getDate() : 0;
            var endDay = view === "day" ? date.getDate() : (view === "week" ? 7 : new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate());
            for (var i = startDay; i < endDay; i++) {
                var currentDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() + i);
                if (currentDate.getDay() === 0 || currentDate < new Date() || isUnpaidLeave(currentDate)) continue;
                for (var j = startHour; j < endHour; j++) {
                    var start = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), j, 0);
                    var end = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate(), j + 1, 0);
                    if (!isSlotOccupied(start, end) && !isPastHour(start)) {
                        scheduler.addEvent({
                            start_date: start,
                            end_date: end,
                            text: text
                        });
                    }
                }
            }
        }
    }
    function isSlotOccupied(start, end) {
        var events = scheduler.getEvents(start, end);
        return events.length > 0;
    }
    function isUnpaidLeave(date) {
        var lastTwoWeeksOfYear = (date.getMonth() === 11 && date.getDate() > 17);
        var lastTwoWeeksOfAugust = (date.getMonth() === 7 && date.getDate() > 17);
        return lastTwoWeeksOfYear || lastTwoWeeksOfAugust;
    }
    function isPastHour(date) {
        var now = new Date();
        now.setMinutes(0);
        now.setSeconds(0);
        return date < now;
    }
   
</script>
</asp:Content>
