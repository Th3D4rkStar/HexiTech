$(document).ready(function () {
    var allOptions = $('#selectCategory option');

    $('#selectType').change(function () {
        $('#selectCategory option').remove();

        var classN = $('#selectType option:selected').prop('class');

        var opts = allOptions.filter('.' + classN);

        $.each(opts, function (i, j) {
            $(j).appendTo('#selectCategory');
        });
    });
});