document.addEventListener("DOMContentLoaded", (e) => {
    
});



function changeLanguage(langCode, flagImageSrc) {

    $.ajax({
        type: "GET", // or "GET" depending on your method
        url: '/Index?handler=SetLanguage', // Update the URL accordingly
        data: { lang: langCode },
        success: function (data) {
            location.reload();
        },
        error: function (err) {
            console.log(err);
        }
    });

    document.querySelector('.lang-control-button').textContent = langCode;
    document.querySelector('#current-language').src = flagImageSrc;
}

document.querySelectorAll('.lang-dropdown a.dropdown-item').forEach(function (item) {
    item.addEventListener('click', function (e) {
        e.preventDefault();

        var langCode = item.textContent;
        var flagImageSrc = item.previousElementSibling.src;

        changeLanguage(langCode, flagImageSrc);
    });
});

