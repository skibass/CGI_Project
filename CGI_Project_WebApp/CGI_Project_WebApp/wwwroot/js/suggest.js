//var toastElList = [].slice.call(document.querySelectorAll('.toast'))
//var toastList = toastElList.map(function (toastEl) {
//  return new bootstrap.Toast(toastEl)
//})

document.addEventListener('DOMContentLoaded', function () {
    var toastElList = [].slice.call(document.querySelectorAll('.toast'));
var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });

    // Optional: Close the toasts after a certain duration
    setTimeout(function () {
        toastList.forEach(function (toast) {
            toast.hide();
        });

        // Ensure the toasts are not reappearing by removing them from the DOM
        toastElList.forEach(function (toastEl) {
            toastEl.parentNode.removeChild(toastEl);
        });
    }, 2000); // 1000 milliseconds = 1 second
});