

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
       console.log(e);
    } 
};
function postAjaxSuccess        (data, status, xhr) {
    try {

        if (data.IsSuccess) {
            postResponseSuccess(data, $(this));

            $(this).trigger("reset");
            var media = $('.post-media', this);
            $(media).addClass('d-none');

            ResetpostsContainer();
        }
         else 
            postResponseError(data,$(this));

    } catch (e) {
       console.log(e);
    } 
    
}; 
function postAjaxFail           (xhr, status, error) {
    try {
        
        if (xhr.responseJSON) 
            postResponseError(xhr.responseJSON,$(this));
        else
            ShowErrorBox("Error in connecting to server");

    } catch (e) {
       console.log(e);
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
       console.log(e);
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
       console.log(e);
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
       console.log(e);
    } 

};
function GotopostErrors         () {
    try {
        window.scrollTo(0,0);
        $('.validation-errors').removeClass('d-none');
    } catch (e) {
       console.log(e);
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

function ResetArticlesContainer () {
    try {
        var nextPage = $('a.next-page').get(0);
        var href = $(nextPage).attr('href');
        var LastSlashIndex = href.lastIndexOf("/");
        var newhref = href.substr(1, LastSlashIndex) + "1";
        $(nextPage).attr('href', newhref);
        $('.posts .post-item').remove();

        Waypoint.refreshAll();

    } catch (e) {
       console.log(e);
    }
};