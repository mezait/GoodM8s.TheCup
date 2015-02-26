$(function () {
    $('#team-edit').on('click', '#add-attendee', function (e) {
        e.preventDefault();

        $.ajax({
            url: this.href,
            success: function (html) {
                $('#attendees tbody').append(html);
            }
        });
    });

    $('#team-edit').on('click', 'a.remove-attendee', function (e) {
        e.preventDefault();

        $(this).parents('tr.attendee:first').remove();
    });
});