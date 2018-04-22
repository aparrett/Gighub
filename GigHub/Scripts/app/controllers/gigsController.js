var GigsController = function (attendanceService) {

    var button;

    var init = function(container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function(e) {
        button = $(e.target);

        if (button.hasClass("btn-default"))
            AttendanceService.createAttendance(button.attr("data-gig-id"), done, fail);
        else
            AttendanceService.deleteAttendance(button.attr("data-gig-id"), done, fail);
    }

    var done = function() {
        var text = (button.text() == "Going") ? "Going?" : "Going";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function() {
        alert("Something failed.");

    };
    return {
        init: init
    }
}(AttendanceService);