$(document).ready(function () {
    let activePage = 1;
    let pages = document.querySelectorAll(".page-item .page-link:not(.pl)");
    const totalPages = pages.length;
    const links = document.querySelectorAll(".page-item .page-link.pl");

    const disableLinks = () => {
        debugger;

        activePage <= 2 ? links[0].classList.add("disable_link") : links[0].classList.remove("disable_link");
        activePage <= 1 ? links[1].classList.add("disable_link") : links[1].classList.remove("disable_link");
        activePage >= totalPages ? links[2].classList.add("disable_link") : links[2].classList.remove("disable_link");
        activePage >= totalPages - 1 ? links[3].classList.add("disable_link") : links[3].classList.remove("disable_link");
    }

    disableLinks();

    const toggleActive = (e) => {
        for (let page of pages) {
            page.classList.contains("active") && page.classList.remove("active");
        }
        e.target.classList.add("active");
        activePage = Number(e.target.firstChild.data);
        disableLinks();
    }

    const switchPage = (e, shuffle) => {
        if (activePage + shuffle < 1 || activePage + shuffle > totalPages) return;
        pages[activePage - 1].classList.remove('active');
        activePage += shuffle;
        pages[activePage - 1].classList.add('active');
        disableLinks();
    };

    for (let page of pages) {
        page.addEventListener('click', toggleActive);
    }

    for (let link of links) {
        console.log("Inside link ");
        link.addEventListener('click', (e) => {
            switch (e.target.id) {
                case 'prev_one':
                    debugger;
                    switchPage(e, -1);
                    break;
                case 'prev_two':
                    debugger;
                    switchPage(e, -2);
                    break;
                case 'next_one':
                    debugger;
                    switchPage(e, 1);
                    break;
                case 'next_two':
                    debugger;
                    switchPage(e, 2);
                    break;
            }
        });
    }
});