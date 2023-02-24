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

//TODO - Recent volunteers js

$(document).ready(function () {
    const prev_btn = document.querySelector(".prev_volunteer>button");
    const next_btn = document.querySelector(".next_volunteer>button");
    const box = document.querySelector(".recent_volunteers_wrapper");
    let current = 2;
    let prev = 1;
    let next = 2;
    let totalVol = 27;
    let totalVolInBox = 9;
    let totalPage = totalVol / totalVolInBox;

    prev_btn.addEventListener('click', () => {
        if (current <= 1) return;
        const vols = document.querySelectorAll('.recent_volunteers_wrapper > *');
        /*debugger;*/
        box.classList.add("d-none");
        vols.forEach(vol => {
            vol.style.overflow = 'hidden';
        })
        box.style.transform = `translateX(-100%)`;
        debugger;
        setTimeout(() => {
            box.style.transform = `translateX(100%)`;
            box.classList.remove('invisible');
            box.style.transform = `translateX(0%)`;
        }, 2000);

        console.log(box)
    })
});