console.log("arrived at vote.js");

document.addEventListener('DOMContentLoaded', function () {
    ConfigToast();
    UpdateProgressBars();

});

function ConfigToast(){
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
    }, 5000); // 1000 milliseconds = 1 second
}
function UpdateProgressBars(){
    var progressBars = document.querySelectorAll('.vote-progress-bar-container');

    progressBars.forEach(function (bar) {
        var percentage = parseFloat(bar.getAttribute('data-percentage'));
        var progressBar = bar.querySelector('.vote-progress-bar');
        
        progressBar.style.width = percentage + '%';
        progressBar.innerHTML = '<p class="vote-progress-percentage m-0">' + percentage.toFixed(1) + '%</p>';
    });
}