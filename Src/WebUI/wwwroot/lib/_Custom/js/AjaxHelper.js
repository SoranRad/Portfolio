
function AjaxBegin          (xhr) {
    xhr.setRequestHeader("XSRF-TOKEN",
        $('input:hidden[name="__RequestVerificationToken"]').val());
};
function AjaxComplete       (xhr, status) {

};
function AjaxSuccess        (data, status, xhr) {
    if (data.IsSuccess) {
        if (data.Redirect) 
            window.location.href = data.Redirect;
        else {
            ParseMessageSuccess(data);
            $('form').trigger("reset");
        }
    } else {

        ParseValidationErrors(data);
    }
};
function AjaxSuccessNoMove  (data, status, xhr) {
    if (data.IsSuccess) {
        if (data.Redirect) 
            window.location.href = data.Redirect;
        else {
            ParseMessageSuccessNoMove(data);
            $('form').trigger("reset");
        }
    } else {
        ParseValidationErrorsNoMove(data);
    }
};
function AjaxFail           (xhr, status, error) {
    
    if (xhr.responseJSON) 
        ParseValidationErrors(xhr.responseJSON);
    else
        ShowErrorBox("Couldn't connect to server");
};
function AjaxAlertFail      (xhr, status, error) {
    ShowErrorBox("Could not connect to server.");
};

function PopulateForm(frm, data) {   

    $.each(data, function(key, value) {  
        var ctrl = $('[name='+key+']', frm);  
        switch(ctrl.prop("type")) { 
        
        case "radio": 
        case "checkbox":   
            ctrl.each(function() {
                if($(this).attr('value') == value) 
                    $(this).attr("checked",value);
            });   
            break;  
        default:
            ctrl.val(value); 
        }  
    });  

};

function ParseMessageSuccessNoMove(data) {
    ResetVlidationErrors();
    ShowVlidationErrorsNoMove();
    $('.validation-errors').removeClass('alert-danger');
    $('.validation-errors').addClass('alert-success');

    $('.validation-errors').append('<span><i class="fas fa-check-circle mx-2"></i>'+data.Message+'</span>');
};
function ParseMessageSuccess(data) {
    ResetVlidationErrors();
    ShowVlidationErrors();
    $('.validation-errors').removeClass('alert-danger');
    $('.validation-errors').addClass('alert-success');

    $('.validation-errors').append('<span><i class="fas fa-check-circle mx-2"></i>'+data.Message+'</span>');
};

function ParseValidationErrors(data) {
    
    ResetVlidationErrors();
    ShowVlidationErrors();

    if (data.Message) {
        $('.validation-errors ul').append('<li>'+data.Message+'</li>');
    }

    if (data.Data ) {
        $.each(data.Data, function(key, value) {

            var item = $('span[data-valmsg-for=\"' + key + '\"]');
            $('input[name=\'' + key + '\']').addClass('input-validation-error');
            $.each(value, function(index, value) {
                var el = '<span class=\'text-danger text-small\'>' + value + '</span>';
                $(item).append(el);
                $('.validation-errors ul').append('<li>'+el+'</li>');
            });

        });
    } 
};
function ParseValidationErrorsNoMove(data) {
    
    ResetVlidationErrors();
    ShowVlidationErrorsNoMove();

    if (data.Message) {
        $('.validation-errors ul').append('<li>'+data.Message+'</li>');
    }

    if (data.Data ) {
        $.each(data.Data, function(key, value) {

            var item = $('span[data-valmsg-for=\"' + key + '\"]');
            $('input[name=\'' + key + '\']').addClass('input-validation-error');
            $.each(value, function(index, value) {
                var el = '<span class=\'text-danger text-small\'>' + value + '</span>';
                $(item).append(el);
                $('.validation-errors ul').append('<li>'+el+'</li>');
            });

        });
    } 
};

function ResetVlidationErrors() {
    $('.validation-errors').empty();
    $('.validation-errors').append('<ul class="m-0"></ul>');
    $('.validation-errors').removeClass('alert-success');
    $('.validation-errors').addClass('alert-danger');
};

function ShowVlidationErrors() {
    window.scrollTo(0,0);
    $('.validation-errors').removeClass('d-none');
};
function ShowVlidationErrorsNoMove() {
    $('.validation-errors').removeClass('d-none');
};

function ParseValidationErrorsCategory(data ,form) {
   if (data.Message) {
        $(form+' span[data-valmsg-for=\"Title\"]').append('<li> <span class=\'text-danger text-small\'>'+data.Message+'</span> </li>');
    }

    if (data.Data ) {
        $.each(data.Data, function(key, value) {

            var item = $(form+' span[data-valmsg-for=\"' + key + '\"]');
            $(form+' input[name=\'' + key + '\']').addClass('input-validation-error');
            $.each(value, function(index, value) {
                var el = '<span class=\'text-danger text-small\'>' + value + '</span>';
                $(item).append(el);
            });

        });
    } 
};
function ShowErrorBox() {
    swal({
        text: "Could not connect to server",
        icon: "error",
        button: "OK!",
    });
};
function ShowErrorBox(message) {
    swal({
        text: message,
        icon: "error",
        button: "OK",
    });
};
function isValidSelector(selector) {
    if (typeof(selector) !== 'string') {
        return false;
    }
    try {
        var $element = $(selector);
    } catch(error) {
        return false;
    }
    return true;
};

