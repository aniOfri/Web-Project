let title = document.getElementById('title');


window.onload = function () {
    title.style.transform = "translateX(" + (-35).toString() + "vh)  translateY(40px)"
}

window.addEventListener('scroll', function () {
    let value = -350 + window.scrollY;
    console.log(value);
    if (value < 600)
        title.style.transform = "translateX(" + (value * 0.1).toString() + "vh)  translateY(40px)"
})
