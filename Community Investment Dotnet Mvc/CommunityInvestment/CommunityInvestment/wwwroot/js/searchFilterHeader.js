'use strict';

let countryList = [...document.querySelectorAll(".countryListClass")];

let themeList = [...document.querySelectorAll(".themeListClass")];

let skillList = [...document.querySelectorAll(".skillListClass")];

let cityList = [];

let badges = {
    title: "",
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

            debugger;
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
        Themes: [],
        Skills: [],
        Cities: [],
        countries: []
    };

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
        },
        error: function (request, error) {
            console.log(request.getAllResponseHeaders())
            alert(request);
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

    /*getAllMissions();*/
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
    let removeFilterBtns = [];

    getAllMissions();


    document.getElementById("searchByMissionTitle").addEventListener("change", function (e) {

        badges.title = e.target.value;
        getAllMissions();
    });

    //Appending Cities, Skills and themes for badges    
    addAndRemoveBadgeWithFilter(countryList, badges.countries, "badgeCountry", "countryFilter");

    addAndRemoveBadgeWithFilter(themeList, badges.themes, "badgeTheme", "themeFilter");

    addAndRemoveBadgeWithFilter(skillList, badges.skills, "badgeSkill", "skillFilter");

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
});

