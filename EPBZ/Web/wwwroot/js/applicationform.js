function addEducationDetails() {
    $("#divEducationDetails").append($("#tempEducationDetails").html());
}

function removeEducationDetailsContainer(buttonElement) {
    buttonElement.closest('.educationDetailsContainer').remove();
}

function removeWorkExperienceContainer(buttonElement) {
    buttonElement.closest('.workExperienceContainer').remove();
}

function addWorkExperience() {
    $("#divWorkExperience").append($("#tempWorkExperience").html());
}

function languageChange(index, language, elm) {
    var checkboxes = document.querySelectorAll('input[type="checkbox"][language="' + language + '"][lan-index="' + index + '"]');
    if (elm && elm.type === 'checkbox' && elm.checked) {
        checkboxes.forEach(function (checkbox) {
            checkbox.disabled = false;
        });

    } else {
        checkboxes.forEach(function (checkbox) {
            checkbox.checked = false;
            checkbox.disabled = true;
        });
    }
}

function techExperienceChange(index, techExperience, elm) {
    var radioboxes = document.querySelectorAll('input[name="techName' + index + techExperience + '"]');
    if (elm && elm.type === 'checkbox' && elm.checked) {
        radioboxes.forEach(function (radio) {
            radio.disabled = false;
        });

    } else {
        radioboxes.forEach(function (radio) {
            radio.checked = false;
            radio.disabled = true;
        });
    }
}

function validateAndSaveData() {
    let bIsValid = true;
    if ($("#inpName").val() == null || $("#inpName").val().length <= 0) {
        $("#errorName").html("Please enter name.");
        bIsValid = false;
    }
    else {
        $("#errorName").html("");
    }

    if ($("#inpEmail").val() == null || $("#inpEmail").val().length <= 0 || !isValidEmail($("#inpEmail").val())) {
        $("#errorEmail").html("Please enter valid email.");
        bIsValid = false;
    }
    else {
        $("#errorEmail").html("");
    }

    if ($("#inpAddress").val() == null || $("#inpAddress").val().length <= 0) {
        $("#errorAddress").html("Please enter address.");
        bIsValid = false;
    }
    else {
        $("#errorAddress").html("");
    }

    if ($("#inpContact").val() == null || $("#inpContact").val().length <= 0) {
        $("#errorContact").html("Please enter contact.");
        bIsValid = false;
    }
    else {
        $("#errorContact").html("");
    }

    let countEducationDetails = $("#divEducationDetails .educationDetailsContainer").length;
    if (countEducationDetails <= 0) {
        $("#errorEducationDetails").html("Add atleast one education detail.");
        bIsValid = false;
    }
    else {
        $("#errorEducationDetails").html("");
    }


    if ($("#inpECTC").val() != null && $("#inpECTC").val().length > 0 && isNaN(parseFloat($("#inpECTC").val()))) {
        $("#errorECTC").html("Please enter valid expected ctc.");
        bIsValid = false;
    }
    else {
        $("#errorECTC").html("");
    }

    if ($("#inpCCTC").val() != null && $("#inpCCTC").val().length > 0 && isNaN(parseFloat($("#inpCCTC").val()))) {
        $("#errorCCTC").html("Please enter valid current ctc.");
        bIsValid = false;
    }
    else {
        $("#errorCCTC").html("");
    }


    if (parseInt($("#inpNoticePeriod").val()) < 0) {
        $("#errorNoticePeriod").html("Please enter valid notice period.");
        bIsValid = false;
    }
    else {
        $("#errorNoticePeriod").html("");
    }


    let objApplication = {
        Id: $("#inpApplicationId").val(),
        Name: $("#inpName").val(),
        Email: $("#inpEmail").val(),
        Address: $("#inpAddress").val(),
        Gender: $('input[name="gender"]:checked').val(),
        Contact: $("#inpContact").val(),
        PreferredLocation: $("#inpLocation").val(),
        ECTC: $("#inpECTC").val(),
        CCTC: $("#inpCCTC").val(),
        Notice: $("#inpNoticePeriod").val(),
    };

    var objEducationDetails = [];

    $('#divEducationDetails .educationDetailsContainer').each(function () {
        var boardValue = $(this).find('.inpBoardUniversity').val();
        var yearValue = $(this).find('.inpYear').val();
        var cgpaValue = $(this).find('.inpCGPA').val();
        var errorSpan = $(this).find('.text-danger');

        console.log(boardValue);
        if (!boardValue || boardValue.trim() === "") {
            errorSpan.html('Board is required');
            bIsValid = false;
        }
        else if (!yearValue || yearValue < 0) {
            errorSpan.html('Invalid year');
            bIsValid = false;
        } else if (!cgpaValue || isNaN(cgpaValue)) {
            errorSpan.html('Invalid CGPA/Percentage.');
            bIsValid = false;
        }
        else {
            var educationDetail = {
                Board: boardValue,
                Year: yearValue,
                CGPA: cgpaValue
            };

            errorSpan.text('');
            objEducationDetails.push(educationDetail);
        }
    });

    console.log("Education Details");
    console.table(objEducationDetails);

    var objWorkExperience = [];

    $('#divWorkExperience .workExperienceContainer').each(function () {
        var inpWorkCompany = $(this).find('.inpWorkCompany').val();
        var inpWorkDesignation = $(this).find('.inpWorkDesignation').val();
        var inpWorkFrom = $(this).find('.inpWorkFrom').val();
        var inpWorkTo = $(this).find('.inpWorkTo').val();
        var errorSpan = $(this).find('.text-danger');

        if (!inpWorkCompany || inpWorkCompany.trim() === "") {
            errorSpan.html('Company is required.');
            bIsValid = false;
        }
        else if (!inpWorkDesignation || inpWorkDesignation.trim() === "") {
            errorSpan.html('Designation is required.');
            bIsValid = false;
        } else if (!inpWorkFrom) {
            errorSpan.html('Work from is required.');
            bIsValid = false;
        }
        else if (!inpWorkTo) {
            errorSpan.html('Work to is required.');
            bIsValid = false;
        }
        else {
            var workExperience = {
                Company: inpWorkCompany,
                Designation: inpWorkDesignation,
                FromDate: inpWorkFrom,
                ToDate: inpWorkTo,

            };

            errorSpan.text('');
            objWorkExperience.push(workExperience);
        }
    });

    console.log("Work Experience");
    console.table(objWorkExperience);

    if (!bIsValid) {
        toastr.error('Please fill all data with proper value.', "Invalid Data");
        return false;
    }

    var techExperiences = [];

    $('.techExperienceContainer').each(function () {
        var tech = $(this).attr('techExperience');
        var techExpIndex = $(this).attr('techExp-index');

        if ($('#cb' + techExpIndex + tech).prop('checked')) {
            var experience = {
                SkillName: tech,
                SkillRate: $('input[name="techName' + techExpIndex + tech + '"]:checked').val()
            };

            techExperiences.push(experience);
        }
    });

    console.log(techExperiences);

    var objLanguages = [];

    $('.languagesContainer').each(function () {
        var language = $(this).attr('language-con');
        var lanIndex = $(this).attr('lan-index-con');

        if ($('#cbLan' + lanIndex + language).prop('checked')) {
            var objLanguage = {
                LanguageName: language,
                CanRead: $('#cbReadLan' + lanIndex + language).prop('checked'),
                CanWrite: $('#cbWriteLan' + lanIndex + language).prop('checked'),
                CanSpeak: $('#cbSpeakLan' + lanIndex + language).prop('checked'),
            };

            objLanguages.push(objLanguage);
        }
    });
    console.log(objLanguages);

    $.ajax({
        type: 'post',
        url: '/ApplicationForm/SaveApplication',
        data: {
            applicationModel: {
                application: objApplication,
                educations: objEducationDetails,
                works: objWorkExperience,
                languages: objLanguages,
                skills: techExperiences,
            }
        },
        success: function (data) {
            toastr.success("Application saved successfully.", "Success");
            setTimeout(function () {
                window.location = "/";
            }, 1200);
        },
        error: function (error) {
            console.log(error.responseText);
            toastr.error(error.responseText, "Error");
        }
    });

}

function isValidEmail(email) {
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}