$("form").each(function () {
    var $this = $(this);
    var hiddenId = $this.attr(`id`);
    var $parent = $this.parent();
    $this.submit(function (e) {
        e.preventDefault();
        var hasErrors = false;
        $this.find(`input[type="text"],input[type="password"],input[type="checkbox"]`).each(function (index, element) {
            if (DetermineAndValidate($this, element)) {
                hasErrors = true;
            }
        });
            $(`[hidden-for="${hiddenId}"]`).val(hasErrors);
    });
});


function DetermineAndValidate(form, element) {
    var elem = $(element);
    var hasErrors = false;
    if (elem.attr(`type`) == "text" || elem.attr(`type`) == `password`|| elem.attr(`type`) == "email") {
        if (elem.hasClass(`required`) && elem.val() == ``) {
            hasErrors = true;
            var inputGroup = elem.attr(`id`);
            var validationMessage = elem.attr('error-message')
            if ($(`[validation-for="${inputGroup}"]`).length == 0) {
                $(`<div class="complement-row" validation-for="${inputGroup}">
                                            <small class="text-danger">${validationMessage}</small>
                                        </div>`).insertAfter(`[input-group='${inputGroup}']`);
            }
            hasErrors = true;
        }
    }
    if (elem.attr(`type`) == "checkbox") {
        if (elem.hasClass(`required`) && !elem.is(`:checked`)) {
            hasErrors = true;
            var inputGroup = elem.attr(`id`);
            var validationMessage = elem.attr('error-message')
            if ($(`[validation-for="${inputGroup}"]`).length == 0) {
                $(`<div class="complement-row" validation-for="${inputGroup}">
                                                <small class="text-danger">${validationMessage}</small>
                                            </div>`).insertAfter(`[input-group='${inputGroup}']`);
            }
            hasErrors = true;
        }
    }
    return hasErrors;
}

$("form").each(function () {
    var $this = $(this);
    var parent = $this.parent();
    var hasErrors = false;
    $this.find(`input[type="text"],input[type="email"], input[type="checkbox"], input[type="password"]`).each(function (index, element) {
        $(element).focus(function () {
            var id = $(element).attr(`id`);
            $(`[validation-for="${id}"]`).remove();
        })
        $(element).click(function () {
            var id = $(element).attr(`id`);
            $(`[validation-for="${id}"]`).remove();
        })
    });
    $this.find(`input.validation-password[type='password']`).each(function (index, element) {
        $(element).on(`input`, function () {
            $(element).tooltip(`show`);
            var lengthPiece = `<b class='text-danger'>✖&nbsp;{Password should have at least 8 characters}.</b><br>`;
            var uppercasePiece = `<b class='text-danger'>✖&nbsp;Password should contain an uppercase letter.</b><br>`;
            var lowercasPece = `<b class='text-danger'>✖&nbsp;Password should contain a lowercase letter.</b><br>`;
            var numberPiece = `<b class='text-danger'>✖&nbsp;Password should contain a number.</b><br>`
            var specialPiece = `<b class='text-danger'>✖&nbsp;Password should contain a special character.</b>`;
            if ($(element).val().length >= 8)
                lengthPiece = `<b class='text-info'>✓&nbsp;Password should have at least 8 characters.</b>`;
            if (/[A-Z]/.test($(element).val()))
                uppercasePiece = `<b class='text-info'>✓&nbsp;Password should contain an uppercase letter.</b>`;
            if (/[a-z]/.test($(element).val()))
                lowercasPece = `<b class='text-info'>✓&nbsp;Password should contain a lowercase letter.</b>`;
            if (/[0-9]/.test($(element).val()))
                numberPiece = `<b class='text-info'>✓&nbsp;Password should contain a number.</b>`;
            if (/[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/.test($(element).val()))
                specialPiece = `<b class='text-info'>✓&nbsp;Password should contain a special character.</b>`
            var popupText = `${lengthPiece}<br>${uppercasePiece}<br>${lowercasPece}<br>${numberPiece}<br>${specialPiece}`;
            $(element).tooltip('hide')
                .attr('data-original-title', popupText)
                .tooltip('show');
        });
    });

    $this.find(`input.validation-password-suplement[type='password']`).each(function (index, element) {
        $(element).on(`input`, function () {
            $(element).tooltip(`show`);
            var tooltipText = `<b class='text-danger'>✖&nbsp;Passwords should match!</b>`;
            var baseId = $(element).attr(`match-for`);
            var basePass = $(`#${baseId}`).val();
            var confirmPass = $(element).val();
            if (basePass == confirmPass) {
                tooltipText = `<b class='text-info'>✓&nbsp;Passwords match!</b>`;
            }
            $(element).tooltip('hide')
                .attr('data-original-title', tooltipText)
                .tooltip('show');
        });
    })
});

