'use strict';

let countryList = [...document.querySelectorAll(".countryListClass")];

let themeList = [...document.querySelectorAll(".themeListClass")];

let skillList = [...document.querySelectorAll(".skillListClass")];

let cityList = [];

let sortByMissionCategory = [];

let badges = {
    title: "",

    sortColumn: "",
    sortOrder: "",
    sortValue:"sortBy",

    //to display badges
    themes: [],
    skills: [],
    cities: [],
    countries: [],
};


function fetchcityBasedOnCountry() {

    let coutryIdsForCities = [];
    for (let country of badges.countries) {
        coutryIdsForCities.push(Number(country.id.replace("badgeCountry", "")));
    }
    $.ajax({
        url: "Home/FetchCityBasedOnCountry",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(coutryIdsForCities),
        dataType: 'html',
        success: function (data) {

            $("#filteredCityList").html(data);
            cityList = [...document.querySelectorAll(".cityListClass")];

            //remove city that was gine by country and add checkbox true if country is selected
            for (let ct of badges.cities) {
                let x = 1;
                for (let fct of cityList) {
                    if (ct.id.replace("badgeCity", "cityFilter").localeCompare(fct.id) == 0) {
                        //checkbox true
                        fct.checked = true;
                        x = 0;
                    }
                }
                if (x == 1) {
                    let badgeId = document.getElementById(ct.id);
                    removeFilterBadge(badgeId);
                }

            }

            addAndRemoveBadgeWithFilter(cityList, badges.cities, "badgeCity", "cityFilter");
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function getAllMissions() {

    let filters = {
        SearchKeyword: badges.title,
        sortColumn: badges.sortColumn || "title",
        sortOrder: badges.sortOrder || "ASC",
        Themes: [],
        Skills: [],
        Cities: [],
        countries: []
    };

    let selectedSortByValue = badges.sortValue;

    for (let theme of badges.themes) {
        filters.Themes.push(Number(theme.id.replace("badgeTheme", "")));
    }

    for (let skills of badges.skills) {
        filters.Skills.push(Number(theme.id.replace("badgeSkill", "")));
    }

    for (let city of badges.cities) {
        filters.Cities.push(Number(city.id.replace("badgeCity", "")));
    }
    for (let country of badges.countries) {
        filters.countries.push(Number(country.id.replace("badgeCountry", "")));
    }

    $.ajax({
        url: "Home/GetAllMissions",
        type: "POST",
        dataType: "html",
        data: JSON.stringify(filters),
        contentType: "application/json",
        success: function (data) {
            $("#missionCardsWrapper").html(data);
            //select sortby true after every ajax call
            debugger;
            const selectBox = document.querySelector(`#sortByMissionList option[value='${selectedSortByValue}']`);
            selectBox.selected = true;
            sortByMissionCategory = [...document.querySelectorAll(".sortByMissionCategory")];
            document.getElementById("sortByMissionList").addEventListener("change", function () {
                switch (this.value) {
                    case "newest":
                        badges.sortColumn = "start_date";
                        badges.sortOrder = "DESC";
                        break;
                    case "oldest":
                        badges.sortColumn = "start_date";
                        badges.sortOrder = "ASC";
                        break;
                    case "lowestAvailableSeats":
                        badges.sortColumn = "lowest_available_seats";
                        badges.sortOrder = "ASC";
                        break;
                    case "highestAvailableSeats":
                        badges.sortColumn = "lowest_available_seats";
                        badges.sortOrder = "DESC";
                        break;
                    case "myFavourites":
                        //TODO:Pending
                        //badges.sortColumn = "lowest_available_seats";
                        //badges.sortOrder = "DESC";
                        break;
                    case "deadline":
                        badges.sortColumn = "deadline";
                        badges.sortOrder = "ASC";
                        break;
                    default:
                        badges.sortColumn = "title";
                        badges.sortOrder = "ASC";
                        break;
                }
                badges.sortValue = this.value;
                debugger;
                getAllMissions();
            });
        },
        error: function (request, error) {
            debugger;
            console.log(request.getAllResponseHeaders())
            console.table({ ...request })
        }
    });
}

function removeFilterBadge(badgeId) {

    console.log(badgeId);

    if (badgeId.id.startsWith("badgeTheme")) {
        let uncheckCheckbox = document.getElementById(badgeId.id.replace("badgeTheme", "themeFilter"));
        uncheckCheckbox.checked = false;

        badges.themes = badges.themes.filter(theme => theme.id !== badgeId.id);
    }
    else if (badgeId.id.startsWith("badgeSkill")) {

        let uncheckCheckbox = document.getElementById(badgeId.id.replace("badgeSkill", "skillFilter"));
        uncheckCheckbox.checked = false;
        badges.skills = badges.skills.filter(skill => skill.id !== badgeId.id)
    }
    else if (badgeId.id.startsWith("badgeCity")) {
        let uncheckCheckbox = document.getElementById(badgeId.id.replace("badgeCity", "cityFilter"));
        if (uncheckCheckbox != null && uncheckCheckbox != undefined) {
            uncheckCheckbox.checked = false;
        }
        badges.cities = badges.cities.filter(city => city.id !== badgeId.id)
    }
    else if (badgeId.id.startsWith("badgeCountry")) {
        let uncheckCheckbox = document.getElementById(badgeId.id.replace("badgeCountry", "countryFilter"));
        uncheckCheckbox.checked = false;
        badges.countries = badges.countries.filter(country => country.id !== badgeId.id)
        fetchcityBasedOnCountry();
    }

    badgeId.remove();
    getAllMissions();
}


//Clear all filters functionality
function clearAllFilterByBtn() {


    for (let badge of badges.themes) {
        let badgeElem = document.getElementById(badge.id);
        removeFilterBadge(badgeElem);
    }
    for (let badge of badges.skills) {
        let badgeElem = document.getElementById(badge.id);
        removeFilterBadge(badgeElem);
    }
    for (let badge of badges.cities) {
        let badgeElem = document.getElementById(badge.id);
        removeFilterBadge(badgeElem);
    }
    for (let badge of badges.countries) {
        let badgeElem = document.getElementById(badge.id);
        removeFilterBadge(badgeElem);
    }
    badges = {
        countries: [],
        themes: [],
        skills: [],
        cities: []
    };
}

//Heart
function addAndRemoveBadgeWithFilter(elemList, badgeList, badgeElem, elemFilter) {
    elemList.forEach(function (elem) {
        elem.addEventListener("change", function () {

            let indexOfCurrentBadge = -1;
            let thisId = this.id;

            badgeList.forEach(function (badge, i) {
                if (badge.id.replace(badgeElem, elemFilter).localeCompare(thisId) == 0) {
                    indexOfCurrentBadge = i;
                }
            })

            if (!this.checked && indexOfCurrentBadge != -1) {
                badgeList.splice(indexOfCurrentBadge, 1);
                document.getElementById(badgeElem + thisId.replace(elemFilter, "")).remove();
            }


            if (this.checked && indexOfCurrentBadge == -1) {
                let badgeId = thisId.replace(elemFilter, badgeElem);
                let removeFilterId = "remove" + badgeId;
                badgeList.push({ id: badgeId, value: this.parentElement.children[1].htmlFor });
                $("#badgeWrapper").prepend(`
                        <div class="mx-2 my-1 d-flex-column flex-row align-items-center py-1 border rounded-pill px-2" id="${badgeId}" >
                            <span class="px-1 text-dark fw-bolder">${this.parentElement.children[1].htmlFor}</span>
                            <span class="btn m-0 p-0 removeFilterX" id="${removeFilterId}" onclick=removeFilterBadge(${badgeId})  ><i class="bi bi-x"></i></span>
                       </div>
                    `);

            }
            debugger;
            if (badgeElem === "badgeCountry") {
                fetchcityBasedOnCountry();
            }

            getAllMissions();

            //Clear all button
            if (badges.themes.length > 0 || badges.skills.length > 0 || badges.cities.length > 0) {
                document.getElementById("clearAllFilter").classList.contains("d-none") ? document.getElementById("clearAllFilter").classList.remove("d-none") : "";
            } else {
                document.getElementById("clearAllFilter").classList.add("d-none")
            }
        });
    });
}

$(document).ready(function () {

    getAllMissions();

    //Search by mission title
    document.getElementById("searchByMissionTitle").addEventListener("change", function (e) {

        badges.title = e.target.value;
        getAllMissions();
    });

    //Appending Cities, Skills and themes for badges    
    addAndRemoveBadgeWithFilter(countryList, badges.countries, "badgeCountry", "countryFilter");

    addAndRemoveBadgeWithFilter(themeList, badges.themes, "badgeTheme", "themeFilter");

    addAndRemoveBadgeWithFilter(skillList, badges.skills, "badgeSkill", "skillFilter");

    //clear all filters
    document.getElementById("clearAllFilter").addEventListener('click', function (e) {
        e.preventDefault();
        clearAllFilterByBtn(e);
        //Clear all button
        if (badges.themes.length > 0 || badges.skills.length > 0 || badges.cities.length > 0) {
            document.getElementById("clearAllFilter").classList.contains("d-none") ? document.getElementById("clearAllFilter").classList.remove("d-none") : "";
        } else {
            document.getElementById("clearAllFilter").classList.add("d-none")
        }
    })

    //logic for pagination
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


        console.log(e.target);

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

