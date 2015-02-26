$(function () {
    $('#score-edit').on('click', '#add-score', function (e) {
        e.preventDefault();

        $.ajax({
            url: this.href,
            success: function (html) {
                $('#scores tbody').append(html);
            }
        });
    });

    $('#score-edit').on('click', 'a.remove-score', function (e) {
        e.preventDefault();

        $(this).parents('tr.score:first').remove();
    });
});