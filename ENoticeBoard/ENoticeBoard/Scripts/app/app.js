var DatetimePicker = function() {
    var applyDateTimePicker = function() {
        $('input[type=datetime]').datepicker({
            dateFormat: "dd/mm/yy",
            changeMonth: true,
            changeYear: true,
            minDate: 0,
            yearRange: "-2:+10"
        });
    };
    return {
        applyDateTimePicker : applyDateTimePicker
    }
}();

var RockController = function () {
    var create = function() {
        $('#btnCreate').click(function () {

            $('#CreateRockModal').modal('show');
        });
    };
    var save = function() {
        $('#btnSave').click(function () {
            var url = $(this).data('url');
            var s;
            if ($('#txtSubject').val().trim() === "") {
                alert('Subject required');
                return;
            }
            if ($('#txtSubject').val().toString().length > 150) {
                alert('Subject too long');
                return;
            }
            var date;
            if ($('#txtDate').val().trim() === "") {
                alert('Due Date required');
                return;

            }
            else {
                date = moment($('#txtDate').val(), "DD/MM/YYYY").toDate();
                s = moment($('#txtDate').val(), "DD/MM/YYYY", true);
                var isValid = s.isValid();
                if (isValid === false) {
                    alert("Invalid date format: " + $('#txtDate').val());
                }
            }
            $.ajax({
                type: "POST",
                url: url,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify({
                    "subject": $("#txtSubject").val(),
                    "priority": $("#ddlPriority").val(),
                    "dateDue": date
                }),
                success: function () {
                    $('#CreateRockModal').modal('hide');
                    location.reload();

                },
                error: function (data) {
                    console.log(data);
                }
            });

        });

    };
    var edit = function () {
        $('.EditRock').click(function () {
            var url = $(this).data('url');
            var id = $(this).attr("rid");
            $.ajax({
                type: "GET",
                url: url,
                data: { "id": id },
                dataType: 'html',
                success: function (data) {

                    $("#EditModalBody").html(data);
                    $("#EditRockModal").modal('show');
                },
                error: function () {
                    alert("Edit Failed");
                }
            });
        });

    };
    var done = function() {
        $('.DoneRock').click(function () {
            var id = $(this).attr("rid");
            var url = $(this).data('url');
            $.ajax({
                type: "GET",
                url: url,
                data: { "id": id },
                dataType: 'html',
                success: function (data) {

                    $("#DoneModalBody").html(data);
                    $("#DoneRockModal").modal('show');
                },
                error: function () {
                    alert("Done Failed");
                }
            });
        });
    };
    return {
        create: create,
        save: save,
        edit: edit,
        done: done
    }
}();




