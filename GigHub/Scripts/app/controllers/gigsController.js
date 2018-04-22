var GigsController = function (attendanceService) {

    var button;

    var init = function(container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
        $(container).on("click", ".js-toggle-follow", toggleFollow);
    };

    var toggleAttendance = function() {
        button = $(e.target);

        if (button.hasClass("btn-default"))
            AttendanceService.createAttendance(button.attr("data-gig-id"), done, fail);
        else
            AttendanceService.deleteAttendance(button.attr("data-gig-id"), done, fail);
    }

    var toggleFollow = function() {
        button = $(e.target);

        $.post("/api/followings", { followeeId: button.attr("data-user-id") })
            .done(function () {
                button.text("Following");
            })
            .fail(function () {
                alert("Something failed");
            });
    }

    var done = function() {
        var text = (button.text() == "Going") ? "Going?" : "Going";

        button.toggleClass("btn-info").toggleClass("btn-default");
    };

    var fail = function() {
        alert("Something failed.");

    };
    return {
        init: init
    }
}(AttendanceService);