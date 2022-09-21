

function postAjaxBegin          (xhr) {
    try {

        var $this   = $(this);
        var token   = $('input:hidden[name="__RequestVerificationToken"]', $this).val();

        xhr.setRequestHeader("XSRF-TOKEN", token);

    } catch (e) {
        console.log(e);

    } 
};

function postAjaxComplete       (xhr, status) {
    try {
        
    } catch (e) {
        Sentry.captureException(e);
    } 
};
function postAjaxSuccess        (data, status, xhr) {
    try {

        if (data.IsSuccess) {
            postResponseSuccess(data, $(this));

            $(this).trigger("reset");
            ResetpostsContainer();
        }
         else 
            postResponseError(data,$(this));

    } catch (e) {
        Sentry.captureException(e);
    } 
    
}; 
function postAjaxFail           (xhr, status, error) {
    try {
        
        if (xhr.responseJSON) 
            postResponseError(xhr.responseJSON,$(this));
        else
            ShowErrorBox("Error in connecting to server");

    } catch (e) {
        Sentry.captureException(e);
    } 
};

function postAjaxUpdateSuccess  (data, status, xhr) {
    try {

        if (data.IsSuccess) {
            var Parent = $(this).closest('.post-item');
            LoadpostContent(Parent);
        }
        else
            postResponseError(data, $(this));

    } catch (e) {
        Sentry.captureException(e);
    } 
};

function ShowpostSuccess        (data) {
    try { 

        ResetVlidationErrors();
        ShowVlidationErrors();
        $('.validation-errors').removeClass('alert-danger');
        $('.validation-errors').addClass('alert-success');

        $('.validation-errors').append('<span><i class="fas fa-check-circle mx-2"></i>'+data.Message+'</span>');
    } catch (e) {
        Sentry.captureException(e);
    } 
};
function ShowpostErrors         (data) {
    
    try {
        
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

    } catch (e) {
        Sentry.captureException(e);
    } 

};
function GotopostErrors         () {
    try {
        window.scrollTo(0,0);
        $('.validation-errors').removeClass('d-none');
    } catch (e) {
        Sentry.captureException(e);
    } 

};

function postResponseSuccess    (data,form) {

    ResetResponseBox(form);  
    var validationBox = $('.validation-errors', form);


    $(validationBox).addClass('alert-success');
    $(validationBox).removeClass('alert-danger');
    $(validationBox).append('<span><i class="fas fa-check-circle mx-2"></i>'+data.Message+'</span>');

};
function postResponseError      (data,form) {
    ResetResponseBox(form);  
    var validationBox = $('.validation-errors', form);

    $(validationBox).addClass('alert-danger');
    $(validationBox).removeClass('alert-success');

    if (data.Message) {
        $(validationBox,'ul').append('<li>'+data.Message+'</li>');
    }

    if (data.Data ) {
        $.each(data.Data, function(key, value) {

            var validatioList = $(validationBox,'ul');
            $.each(value, function(index, value) {
                var el = '<span class=\'text-danger text-small\'>' + value + '</span>';
                $(validatioList).append('<li>'+el+'</li>');
            });

        });
    } 
};

function ResetResponseBox       (form) {
    var validationBox = $('.validation-errors', form);
    $(validationBox).empty();
    $(validationBox).append('<ul class="m-0"></ul>');
    $(validationBox).removeClass('d-none');
    $([document.documentElement, document.body]).animate({
        scrollTop : $(validationBox).offset().top - 250
    }, 1000);
};
