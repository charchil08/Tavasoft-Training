'use strict';

let themeList = [...document.querySelectorAll(".themeListClass")];

let skillList = [...document.querySelectorAll(".skillListClass")];

let cityList = [];

let badges = {
    themes: [],
    skills: [],
    cities: [],
};

function fetchcityBasedOnCountry(countryId) {
    $.ajax({
        url: "Home/FetchCityBasedOnCountry",
        type: "GET",
        data: {
            "countryId": countryId
        },
        dataType: 'html',
        success: function (data) {
            debugger;
            $("#filteredCityList").html(data);
            cityList = [...document.querySelectorAll(".cityListClass")];
            addAndRemoveBadgeWithFilter(cityList, badges.cities, "badgeCity", "cityFilter");
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function removeFilterBadge(badgeId) {
    debugger;
    console.log(badgeId);

    if (badgeId.id.startsWith("badgeTheme")) {
        let uncheckCheckbox = document.getElementById(badgeId.id.replace("badgeTheme", "themeFilter"));
        uncheckCheckbox.checked = false;
        badges.themes.filter(theme => theme.id !== badgeId.id);
    }
    else if (badgeId.id.startsWith("badgeTheme")) {
        let checkboxId = badgeId.id.replace("badgeSkill", "skillFilter");
        let uncheckCheckbox = document.getElementById(checkboxId);

        uncheckCheckbox.checked = false;
        badges.skills.filter(skill => skill.id !== badgeId.id)
    }
    else {
        let checkboxId = badgeId.id.replace("badgeCity", "cityFilter");
        let uncheckCheckbox = document.getElementById(checkboxId);

        uncheckCheckbox.checked = false;
        badges.skills.filter(skill => skill.id !== badgeId.id)
    }

    badgeId.remove();
}


//Clear all filters functionality
function clearAllFilterByBtn() {
    debugger;

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
    badges = {
        themes: [],
        skills: [],
        cities: []
    };
}

function addAndRemoveBadgeWithFilter(elemList, badgeList, badgeElem, elemFilter) {
    elemList.forEach(function (elem) {
        elem.addEventListener("change", function () {
            debugger;
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
    document.getElementById("selectCountryFilter").addEventListener('change', (e) => {
        e.preventDefault();
        console.log(e.target.value)
        debugger;
        fetchcityBasedOnCountry(e.target.value);
    });

    let removeFilterBtns = [];

    //Appending Cities, Skills and themes for badges    
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