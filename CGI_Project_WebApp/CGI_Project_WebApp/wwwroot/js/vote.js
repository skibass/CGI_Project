$(document).ready(function () {
    $('[data-bs-toggle="collapse"]').collapse();

    $(".vote-progress-bar-container").each(function () {
        var percentage = parseFloat($(this).data("percentage"));
        var progressBar = $(this).find(".progress-bar");

        // Check if the percentage is a valid number
        if (!isNaN(percentage)) {
            progressBar.css("width", percentage + "%");
        }
    });
});

var changeChevron = function (event) {
    var container = $(event.target).closest('.vote-item');
    var chevron = container.find('.chevron.down');
    chevron.toggleClass('up');
}

var votePercentages = @Html.Raw(Json.Serialize(Model.VotePercentages));

// Update progress bars based on the calculated percentages
Object.keys(votePercentages).forEach(function (suggestionId) {
    var percentage = votePercentages[suggestionId];
    var progressBar = document.getElementById("progress-bar-" + suggestionId);
    progressBar.style.width = percentage + "%";
    progressBar.innerHTML = percentage.toFixed(2) + "%";
});