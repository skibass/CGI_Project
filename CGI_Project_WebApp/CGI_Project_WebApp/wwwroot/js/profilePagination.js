document.addEventListener("DOMContentLoaded", function () {
    var currentPage = 1;
    var itemsPerPage = 5;

    function showPage(pageNumber) {
        var items = document.querySelectorAll("#votesContainer > .vote-info");
        var totalPages = Math.ceil(items.length / itemsPerPage);

        for (var i = 0; i < items.length; i++) {
            items[i].style.display = "none";
        }

        for (var i = (pageNumber - 1) * itemsPerPage; i < pageNumber * itemsPerPage && i < items.length; i++) {
            items[i].style.display = "block";
        }

        var pagination = document.querySelector(".pagination");
        pagination.innerHTML = "";

        for (var i = 1; i <= totalPages; i++) {
            var pageLink = document.createElement("a");
            pageLink.innerHTML = i;
            pageLink.href = "#";
            pageLink.addEventListener("click", function (event) {
                event.preventDefault();
                showPage(parseInt(this.innerHTML));
            });

            pagination.appendChild(pageLink);
        }
    }

    showPage(currentPage);
});