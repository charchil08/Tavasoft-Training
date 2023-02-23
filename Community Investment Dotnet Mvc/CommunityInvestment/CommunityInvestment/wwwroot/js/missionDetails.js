//Dropdown slider script
$(document).ready(function () {
    const slides = document.querySelectorAll(".slide")
    const vid = document.querySelectorAll("video.slide")
    let totalImgInSlider = 4;
    const totalImg = 6;
    var counter = 0;

    const leftChanges = () => {
        slides.forEach((slider, index) => {
            slider.style.left = `${index * 100 / totalImgInSlider}%`
        })
    }

    window.addEventListener('resize', () => {
        if (window.innerWidth < 576) {
            totalImgInSlider = 1;
        } else {
            totalImgInSlider = 4;
        }
        leftChanges();
    }, true);

    leftChanges();

    slideImg = () => {
        slides.forEach((slider) => {
            slider.style.transform = `translateX(-${counter * 100}%)`
        })
    }

    goPrev = () => {
        if (counter != 0)
            counter--;
        slideImg();
    }

    goNext = () => {
        if (counter < totalImg - totalImgInSlider)
            counter++;
        slideImg();
    }

    setImage = () => {
        document.getElementById("demo").src = document.querySelector(".slide.active").src;
    }
});