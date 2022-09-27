
var Common = {
    sound_Max_File_Size : 1024 * 1024 * 5,
    Init: function () {

        InitPlayer      ();
        InputAutoHeight();
        ShowUpButton();
        FixNavBarOnScroll();
        //InitValidation();

    },
};
function InitValidation     () {

    jQuery.validator.setDefaults({
        
        ignore: ":hidden"
    });
    
};
function ChangePic          () {
    if (!$('.FilePic').length)
        return;

    $("#change-link,.change-img").click(function (e) {
        e.preventDefault();
        var input = $(this).attr('data-input');
        if (input == null)
            input = '#FileLogo';
        $(input).click();
    });
    $('#FileLogo,.FilePic').change(function (e) {
        e.preventDefault();

        var input = $(this).attr('data-preview');
        if (input)
            readURL(this, input);

        var showitem = $(this).attr('data-show-item');
        if (showitem)
            $(showitem).removeClass('d-none');
    });

};
function ReadImage          (file) {
    try {
        var fr = new FileReader();
        var image = new Image();

        return new Promise((resolve, reject) => {

            fr.onload = (e) => {
                image.src = e.target.result;
            };
            image.onload = function () {
                resolve({

                    result: true,
                    message: "",
                    data: image,

                });
            };

            image.onerror = (error) => reject({ result: false, message: "Error in loading the image" });
            fr.onerror = (error) => reject({ result: false, message: "Error in loading the image" });

            fr.readAsDataURL(file);
        });


    } catch (e) {
         console.log(e);
    }

}
function InitPlayer         () {
    const player = new Plyr(document.querySelector('.audio-player'));
};
function readURL            (input, elementId) {
    if (input.files && input.files[0]) {
        var file = input.files[0];

        if (file.size > Common.image_Max_File_Size) { 
            swal('Youre file is too big,More than 3 MB');
            return;
        }

        var reader = new FileReader();
        reader.onload = function (e) {
            $(elementId).attr('src', e.target.result);
        }
        reader.readAsDataURL(file);
    } else
        $(elementId).attr('src', "");
};
function readURLByElement   (input, element) {
    try {

        if (input.files && input.files[0]) {
            var file = input.files[0];

            if (file.size > Common.image_Max_File_Size) {
                swal('Youre file is too big,More than 3 MB');
                return;
            }

            var reader = new FileReader();
            reader.onload = function (e) {
                $(element).attr('src', e.target.result);
            }
            reader.readAsDataURL(file);
        } else
            element.attr('src', "");

    } catch (e) {
         console.log(e);
    }
};
function isEmptyOrSpaces    (input) {
    if (typeof input === 'undefined' || input == null) return true;

    return input.replace(/\s/g, '').length < 1;
}
function InputAutoHeight    () {
    $('.auto-height').autogrow({ vertical: true, horizontal: false });
};
function ReadImage          (file) {
    try {
        var fr = new FileReader();
        var image = new Image();

        return new Promise((resolve, reject) => {

            fr.onload = (e) => {
                image.src = e.target.result;
            };
            image.onload = function () {
                resolve({

                    result: true,
                    message: "",
                    data: image,

                });
            };

            image.onerror = (error) => reject({ result: false, message: "Error in loading the image" });
            fr.onerror = (error) => reject({ result: false, message: "Error in loading the image" });

            fr.readAsDataURL(file);
        });


    } catch (e) {
         console.log(e);
    }

}
function ShowUpButton       () {
    try {
        var wind = $(window);
        wind.on("scroll",
            function () {
                var bodyScroll = wind.scrollTop(),
                    button_top = $(".butn-top");
                if (bodyScroll > 1000) {
                    button_top.addClass("butn-show");
                } else {
                    button_top.removeClass("butn-show");
                }
            });
        $(".butn-top").on('click',
            function (e) {
                e.preventDefault();
                $('html,body').animate({
                    scrollTop: 0
                },
                    700);
            });
    }
    catch (e) {
        console.log(e);
    }
};
function FixNavBarOnScroll  () {
 
    var wind = $(window);
    wind.on("scroll", function () {

        var bodyScroll = wind.scrollTop(),
            navbar = $("#main-header");

        if (bodyScroll > 180)
            navbar.addClass("fixed-top");
        else
            navbar.removeClass("fixed-top");

    });
};