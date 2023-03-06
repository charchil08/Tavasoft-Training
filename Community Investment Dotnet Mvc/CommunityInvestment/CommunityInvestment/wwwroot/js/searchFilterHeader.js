'use strict';

let badges = {
    themes: [],
    skills: []
};

function removeFilterBadge(badgeId) {
    debugger;
    console.log(badgeId);

    if (badgeId.id.startsWith("badgeTheme")) {
        let uncheckCheckbox = document.getElementById(badgeId.id.replace("badgeTheme", "themeFilter"));
        uncheckCheckbox.checked = false;
        badges.themes.filter(theme => theme.id !== badgeId.id);
    }
    else {
        let checkboxId = badgeId.id.replace("badgeSkill", "skillFilter");
        let uncheckCheckbox = document.getElementById(checkboxId);

        uncheckCheckbox.checked = false;
        badges.skills.filter(skill => skill.id !== badgeId.id)
    }

    badgeId.remove();
}

//Clear all filters functionality
function clearAllFilterByBtn() {
    debugger;
    document.getElementById("clearAllFilter").addEventListener('click', function () {
        if (badges.themes > 0) console.log("legth");
    });

}


$(document).ready(function () {
    document.getElementById("selectCountryFilter").addEventListener('change', (e) => {
        e.preventDefault();
        console.log(e.target.value)
        debugger;
        fetchcityBasedOnCountry(e.target.value);
    });

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
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    }

    let themeList = [...document.querySelectorAll(".themeListClass")];
    console.log("Theme list : ", themeList);

    let skillList = [...document.querySelectorAll(".skillListClass")];
    console.log("Skill list : ", skillList);

    let removeFilterBtns = [];

    //Appending Skills and themes for badges    
    themeList.forEach(function (theme, ind) {
        theme.addEventListener("change", function () {
            debugger;

            let indexOfCurrentBadge = -1;
            let thisId = this.id;

            badges.themes.forEach(function (t, i) {
                if (t.id.replace("badgeTheme", "themeFilter").localeCompare(thisId) == 0) {
                    indexOfCurrentBadge = i;
                }
            })

            if (!this.checked && indexOfCurrentBadge != -1) {
                badges.themes.splice(indexOfCurrentBadge, 1);
                document.getElementById("badgeTheme" + thisId.replace("themeFilter", "")).remove();
            }

            if (this.checked && indexOfCurrentBadge == -1) {
                let badgeId = thisId.replace("themeFilter", "badgeTheme");
                let removeFilterId = "remove" + badgeId;
                badges.themes.push({ id: badgeId, value: this.parentElement.children[1].htmlFor });

                $("#badgeWrapper").prepend(`
                        <div class="mx-2 my-1 d-flex-column flex-row align-items-center py-1 border rounded-pill px-2" id="${badgeId}" >
                            <span class="px-1 text-dark fw-bolder">${this.parentElement.children[1].htmlFor}</span>
                            <span class="btn m-0 p-0 removeFilterX" id="${removeFilterId}" onclick=removeFilterBadge(${badgeId})  ><i class="bi bi-x"></i></span>
                       </div>
                    `);
            }

            //Clear all button
            if (badges.themes.length > 0 || badges.skills.length > 0) {
                document.getElementById("clearAllFilter").classList.contains("d-none") ? document.getElementById("clearAllFilter").classList.remove("d-none") : "";
            } else {
                document.getElementById("clearAllFilter").classList.add("d-none")
            }
        });
    });

    skillList.forEach(function (skill, ind) {
        skill.addEventListener("change", function () {
            debugger;

            let indexOfCurrentBadge = -1;
            let thisId = this.id;

            badges.skills.forEach(function (s, i) {
                if (s.id.replace("badgeSkill", "skillFilter").localeCompare(thisId) == 0) {
                    indexOfCurrentBadge = i;
                }
            })

            if (!this.checked && indexOfCurrentBadge != -1) {
                badges.skills.splice(indexOfCurrentBadge, 1);
                document.getElementById("badgeSkill" + thisId.replace("skillFilter", "")).remove();
            }


            if (this.checked && indexOfCurrentBadge == -1) {
                let badgeId = thisId.replace("skillFilter", "badgeSkill");
                let removeFilterId = "remove" + badgeId;
                badges.skills.push({ id: badgeId, value: this.parentElement.children[1].htmlFor });
                $("#badgeWrapper").prepend(`
                        <div class="mx-2 my-1 d-flex-column flex-row align-items-center py-1 border rounded-pill px-2" id="${badgeId}" >
                            <span class="px-1 text-dark fw-bolder">${this.parentElement.children[1].htmlFor}</span>
                            <span class="btn m-0 p-0 removeFilterX" id="${removeFilterId}" onclick=removeFilterBadge(${badgeId})  ><i class="bi bi-x"></i></span>
                       </div>
                    `);
            }

            //Clear all button
            if (badges.themes.length > 0 || badges.skills.length > 0) {
                document.getElementById("clearAllFilter").classList.contains("d-none") ? document.getElementById("clearAllFilter").classList.remove("d-none") : "";
            } else {
                document.getElementById("clearAllFilter").classList.add("d-none")
            }
        });
    });

});
