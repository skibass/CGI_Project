document.addEventListener("DOMContentLoaded", function () {
    var currentPage = 1;
    var itemsPerPage = 3; 

    function showPage(pageNumber) {
        var items = document.querySelectorAll("#photoContainer > .photo-item");
        var totalPages = Math.ceil(items.length / itemsPerPage);

        items.forEach(item => item.style.display = 'none');
        for (var i = (pageNumber - 1) * itemsPerPage; i < pageNumber * itemsPerPage && i < items.length; i++) {
            items[i].style.display = 'block';
        }

        var pagination = document.querySelector(".pagination");
        pagination.innerHTML = '';
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
