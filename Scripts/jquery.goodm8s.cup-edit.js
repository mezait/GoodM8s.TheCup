$(function () {
    $('#cup-edit').on('click', '#add-event', function (e) {
        e.preventDefault();

        $.ajax({
            url: this.href,
            success: function (html) {
                $('#events tbody').append(html);
            }
        });
    });

    $('#cup-edit').on('click', 'a.remove-event', function (e) {
        e.preventDefault();

        $(this).parents('tr.event:first').remove();
    });

    $('#cup-edit').on('click', '#add-place', function (e) {
        e.preventDefault();

        $.ajax({
            url: this.href,
            success: function (html) {
                $('#places tbody').append(html);
            }
        });
    });

    $('#cup-edit').on('click', 'a.remove-place', function (e) {
        e.preventDefault();

        $(this).parents('tr.place:first').remove();
    });
});