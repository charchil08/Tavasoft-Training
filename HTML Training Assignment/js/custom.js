"use strict"
const menuItems = [
    {
        src: "/images/dashboard-active.svg",
        alt: "",
        text: "Dashboard",
        className: "img_dashboard"
    },
    {
        src: "./images/graph-default.svg",
        alt: "",
        text: "Analysis",
        className: "img_analysis"
    },
    {
        src: "./images/inbox-default.svg",
        alt: "",
        text: "Inbox",
        className: "img_inbox"
    },
    {
        src: "./images/clock-default.svg",
        alt: "",
        text: "Histroy",
        className: "img_histroy"
    },
    {
        src: "./images/user-default.svg",
        alt: "",
        text: "Users",
        className: "img_histroy"
    },
    {
        src: "./images/maintenance-default.svg",
        alt: "",
        text: "Maintenance",
        className: "img_maintenance"
    },
    {
        src: "./images/setting-default.svg",
        alt: "",
        text: "Setting",
        className: "img_setting"
    }
]

let salesCards = [
    {
        img: './images/star.svg',
        number_of_products: 23456,
        desc: 'New Products',
        img_class: 'img img_new ',
    },
    {
        img: './images/new-product.svg',
        number_of_products: 2345,
        desc: 'Added Products',
        img_class: 'img img_added ',
    },
    {
        img: './images/total-product.svg',
        number_of_products: 32456,
        desc: 'Total Products',
        img_class: 'img img_total_products ',
    },
    {
        img: './images/dollar-filled.svg',
        number_of_products: 5678,
        desc: "Today's Sale",
        img_class: 'img img_today ',
    },
    {
        img: './images/money-bag.svg',
        number_of_products: 335342,
        desc: 'Total Sale',
        img_class: 'img img_total_sale '
    }
];

arrangeSidebar();
arrangeSalesCards();
responsiveSidebar();

function responsiveSidebar() {
    const hamburger = document.querySelector(".img_menubar");
    const sidebar = document.querySelector(".sidebar");

    hamburger.addEventListener('click', (e) => {
        e.preventDefault();
        if (sidebar.classList.contains('close')) {
            sidebar.classList.remove('close');
            sidebar.classList.add('open');
        } else {
            sidebar.classList.remove('open');
            sidebar.classList.add('close');
        }
    })

}


function arrangeSidebar() {
    const sidebar = document.querySelector('.sidebar');
    console.log(sidebar);

    for (let i = 0; i < menuItems.length; i++) {
        let div = document.createElement('div');
        div.innerHTML = ` <p> <img class=${menuItems[i].className} src=${menuItems[i].src} alt=${menuItems[i].alt} /> ${menuItems[i].text}</p>`;
        div.classList.add('sidebar_item', `sidebar_item_${i + 1}`)
        sidebar.append(div);
    }
}

function arrangeSalesCards() {
    // format number_of_products
    for (let i = 0; i < salesCards.length; i++) {
        salesCards[i].number_of_products = new Intl.NumberFormat({
            maximumSignificantDigits: 3,
            maximumSignificantDigits: 2,
        }).format(salesCards[i].number_of_products);
    }


    const cards = document.querySelector('.sales_cards');

    for (let i = 0; i < salesCards.length; i++) {
        const { img, number_of_products, img_class, desc } = salesCards[i];
        let div = document.createElement('div');
        div.classList.add('card_item', `card_item_${i + 1}`);
        div.innerHTML = `<img src=${img} class=${img_class} alt="" />`;

        let div_right = document.createElement('div');
        div_right.classList.add('div_right');
        div_right.innerHTML = `
        <p class='number_of_products'>${number_of_products}</p>
        <p class='desc'>${desc}</p>`;

        div.append(div_right);
        cards.append(div);
    }

    console.log(cards);

}