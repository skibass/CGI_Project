
function changeLanguage(langCode, flagImageSrc) {

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

