//const countDate = new Date('Nov 20,2021 17:00:00').getTime();
var countDate = new Date(moment(document.getElementById('finalTime').value, 'DD-MM-YYYY HH:mm:ss'));
var intervalId, intervarNow;

function clear() {
    clearInterval(intervalId);
    clearInterval(intervarNow);
    Swal.fire({
        title: 'Oops!',
        text: 'İhale Bitmiştir!',
        icon: 'warning',
        showConfirmButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false,
        showConfirmButton: true,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'İhale Listelerine Dön!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Purchase/Tender'
        }
    });
}

function padleft(num, targetLength) {
    return num.toString().padStart(targetLength, 0);
}

function countDown() {
    const now = new Date().getTime();
    let gap = countDate - now;

    let second = 1000;
    let minute = second * 60;
    let hour = minute * 60;
    let day = hour * 24;

    let d = Math.floor(gap / (day));
    let h = Math.floor((gap % (day)) / (hour));
    let m = Math.floor((gap % (hour)) / (minute));
    let s = Math.floor((gap % (minute)) / (second));

    document.getElementById('day').innerText = padleft(d, 2);
    document.getElementById('hour').innerText = padleft(h, 2);
    document.getElementById('minute').innerText = padleft(m, 2);
    document.getElementById('second').innerText = padleft(s, 2);
}

intervalId = setInterval(function () {
    countDate > (new Date())
        ? countDown(countDate)
        : clear()
}, 1000);

intervarNow = setInterval(function () {
    document.getElementById('todayDate').innerText = moment().format('DD.MM.YYYY');
    document.getElementById('todayTime').innerText = moment().format('HH:mm:ss');
}, 1000);


