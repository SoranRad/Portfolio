var infinite;
var Post = {

    IsContentFirst: false,
    Init: function () {

        if (!$('#post-create').length)
            return;

        
        PostButtonActions();
        InitEditor($('#new-post'));

        SetuppostsLoading();
    },
};

function PostButtonActions      () {

    AttachButtons();
    TagButtonsClick();

    ImageInputClick();
    SoundInputClick();
    VideoLinkChange();

    ResetForm();

    $('#btn-new-post').click(function (e) {
        try {
            e.preventDefault();
            $('.post-card').hide();
            $('#post-create').slideDown("slow");
            $([document.documentElement, document.body]).animate({
                scrollTop: $('#post-create').offset().top - 150
            }, 2000);
            return false;
        } catch (e) {
            console.log(e);
        }

    });
    //$('#btn-new-camp').click(function (e) {
    //    try {
    //        e.preventDefault();
    //        $('.post-card').hide();
    //        $('#post-camp').slideDown("slow");
    //        $([document.documentElement, document.body]).animate({
    //            scrollTop: $('#post-camp').offset().top
    //        }, 2000);
    //        return false;
    //    } catch (e) {
    //        console.log(e);
    //    }

    //});


    //$('.show-comment').click(function (e) {
    //    try {
    //        e.preventDefault();
    //        var footer = $(this).parents('.card-footer').get(0);
    //        var commentSection = $(footer).find('.comments').get(0);
    //        $(commentSection).toggle();
    //        return false;
    //    } catch (e) {
    //        console.log(e);
    //    }

    //});
    //$('.show-likes').click(function (e) {
    //    try {
    //        e.preventDefault();
    //        var footer = $(this).parents('.card-footer').get(0);
    //        var likeSection = $(footer).find('.likes').get(0);
    //        $(likeSection).toggle();
    //        return false;
    //    } catch (e) {
    //        console.log(e);
    //    }
    //});

};

function TagButtonsClick        () {
    //=====Tags and Lable===
    $(".posts").on(
        "click",
        ".post-card .post-form .card-footer .row .attach-row .btn-tag",
        function (e) {
            try {
                e.preventDefault();
                var form = $(this).closest('form');
                var tagSection = $(form).find('.tag-section').get(0);
                $(tagSection).slideToggle("slow");
                return false;

            } catch (e) {
                console.log(e);
            }
        });
    $(".posts").on(
        "click",
        ".post-card .post-form .card-footer .tag-section .remove-tags",
        function (e) {
            try {
                e.preventDefault();
                var inputTag = $(this).prev();
                $(inputTag).tagsinput('removeAll');
            } catch (e) {
                console.log(e);
            }
        });
};
function AttachButtons          () {

    $(".posts").on(
        "click",
        ".post-card .post-form .card-footer .row .attach-row .btn-attach-image",
        ImageAttachClick);

    $(".posts").on(
        "click",
        ".post-card .post-form .card-footer .row .attach-row .btn-attach-sound",
        SoundAttachClick);

    $(".posts").on(
        "click",
        ".post-card .post-form .card-footer .row .attach-row .btn-attach-video-link",
        VideoAttachClick);


    $(".posts").on(
        "click",
        ".post-card .post-form .card-body .post-media .remove-attach",
        function (e) {

            e.preventDefault();
            RemovepostMedia($(this));

        });
     
};

function SetMediaPosition       ($this) {
    try {

        var form = $this.closest('form');
        var contentInput = $(form).find('.content-editor').get(0);
        var contentQuill = Quill.find(contentInput);

        var contentArea = $(form).find('.post-text').get(0);
        var isContentFirst = $(form).find('input[name="IsContentFirst"]').get(0);
        var postMedia = $(form).find('.post-media').get(0);

        postMedia = $(postMedia).detach();

        if (contentQuill && contentQuill.getText().trim().length === 0) {
            $(contentArea).before(postMedia);
            $(isContentFirst).attr("value", "False");
        } else {
            $(contentArea).after(postMedia);
            $(isContentFirst).attr("value", "True");
        }
        $(postMedia).removeClass('d-none');

    } catch (e) {
        console.log(e);
    }
};
function SetMediaPreview        (kind, form) {
    try {

        var imagePreview = $(form).find('.image-attached').get(0);
        var soundPreview = $(form).find('.audio-attached').get(0);
        var videoLinkPreview = $(form).find('.videoLink').get(0);

        switch (kind) {
            case "image":
                $(imagePreview).removeClass('d-none');
                $(soundPreview).addClass('d-none');
                $(videoLinkPreview).addClass('d-none');
                break;
            case "soundFile":
                $(imagePreview).addClass('d-none');
                $(soundPreview).removeClass('d-none');
                $(videoLinkPreview).addClass('d-none');
                break;
            case "videoLink":
                $(imagePreview).addClass('d-none');
                $(soundPreview).addClass('d-none');
                $(videoLinkPreview).removeClass('d-none');
                break;
        }
    } catch (e) {
        console.log(e);
    }

};
function RemovepostMedia        ($this) {
    try {
        var form = $this.closest('form');
        var postMedia = $(form).find('.post-media').get(0);
        var isContentFirst = $(form).find('input[name="IsContentFirst"]').get(0);
        var FileDeleted = $(form).find('input[name="FileDeleted"]').get(0);


        ClearMediaFiles("All", form)
        $(postMedia).addClass('d-none');
        $(isContentFirst).attr("value", "False");

        if (FileDeleted)
            $(FileDeleted).attr("value", "True");

    } catch (e) {
        console.log(e);
    }
};

function ClearMediaFiles        (kind, form) {
    try {

        var imageFile = $(form).find('input[name="PictureFile"]').get(0);
        var soundFile = $(form).find('input[name="SoundFile"]').get(0);
        var videoLinkFile = $(form).find('input[name="VideoLink"]').get(0);
        var FileDeleted = $(form).find('input[name="FileDeleted"]').get(0);

        if (FileDeleted)
            $(FileDeleted).attr("value", "True");

        switch (kind) {
            case "image":

                $(soundFile).val('');
                $(videoLinkFile).val('');
                break;
            case "soundFile":

                Cropper.CroppeBlob = null;
                $(imageFile).val('');
                $(videoLinkFile).val('');
                break;
            case "videoLink":

                Cropper.CroppeBlob = null;
                $(imageFile).val('');
                $(soundFile).val('');
                break;
            case "All":

                Cropper.CroppeBlob = null;
                $(imageFile).val('');
                $(soundFile).val('');
                $(videoLinkFile).val('');
                break;
        }

    } catch (e) {
        console.log(e);
    }

};
function ClearMediaFileAndPreview(kind, form) {

    try {
        var imagePreview = $(form).find('.image-attached').get(0);
        var soundPreview = $(form).find('.audio-attached').get(0);
        var videoLinkPreview = $(form).find('.videoLink').get(0);
        var postMedia = $(form).find('.post-media').get(0);
        var FileDeleted = $(form).find('input[name="FileDeleted"]').get(0);

        if (FileDeleted)
            $(FileDeleted).attr("value", "True");

        switch (kind) {
            case "image":
                var imageFile = $(form).find('input[name="PictureFile"]').get(0);

                $(imagePreview).addClass('d-none');
                $(imageFile).val('');

                break;
            case "soundFile":
                var soundFile = $(form).find('input[name="SoundFile"]').get(0);

                $(soundFile).val('');
                $(soundPreview).addClass('d-none');

                break;
            case "videoLink":
                var videoLinkFile = $(form).find('input[name="VideoLink"]').get(0);

                $(videoLinkFile).val('');
                $(videoLinkPreview).addClass('d-none');
                break;

            case "All":
                //=================================
                var imageFile = $(form).find('input[name="PictureFile"]').get(0);
                $(imagePreview).addClass('d-none');
                $(imageFile).val('');
                //==================================
                var soundFile = $(form).find('input[name="SoundFile"]').get(0);
                $(soundFile).val('');
                $(soundPreview).addClass('d-none');
                //==================================
                var videoLinkFile = $(form).find('input[name="VideoLink"]').get(0);
                $(videoLinkFile).val('');
                $(videoLinkPreview).addClass('d-none');
                break;
        }

        if (
            $(imagePreview).hasClass('d-none')
            && $(soundPreview).hasClass('d-none')
            && $(videoLinkPreview).hasClass('d-none')
        )

            $(postMedia).addClass('d-none');


    } catch (e) {
        console.log(e);
    }


};

function NormalizeVideoURL      (str) {
    var temp = str.toLowerCase();
    if (temp.includes("?playlist"))
        str = str.substring(0, temp.indexOf("?playlist"));
    return str;
}

function ImageInputClick        () {
    try {

        $(".posts").on(
            "change",
            ".post-card .post-form .image-input",

            //$('.image-input').change(
            function (e) {
                e.preventDefault();
                var form = $(this).closest('form');
                var $this = $(this);
                var mainInput = $(form).find('input[name="PictureFile"]').get(0);

                if (!this.files[0]) {

                    ClearMediaFileAndPreview("image", form);
                    return;
                }

                ReadImage(this.files[0])
                    .then(function (e) {

                        Cropper.Init();
                        Cropper.Bind(e.data);
                        Cropper.Show()
                            .then(function (e) {

                                ClearMediaFiles("image", form);
                                SetMediaPreview("image", form);
                                SetMediaPosition($this);

                                var imageAttached = $(form).find('.image-attached').get(0);
                                var urlCreator = window.URL || window.webkitURL;
                                var imageUrl = urlCreator.createObjectURL(e.image);
                                $(imageAttached).attr('src', imageUrl);


                                var file = new File([e.image], 'image.' + Cropper.Extention, { type: "image/" + Cropper.Extention, lastModified: new Date().getTime() });
                                var container = new DataTransfer();
                                container.items.add(file);
                                $($this).val('');
                                mainInput.files = container.files;
                                //$($this2).val(null);
                            })
                            .catch(function (e) {

                            });
                    })
                    .catch(function (e) {
                        swal('خطا', 'خطا در نمایش تصویر', 'error');
                    })
                    ;
            });
    } catch (e) {
        console.log(e);
    }
};
function SoundInputClick        () {
    try {

        $(".posts").on(
            "change",
            ".post-card .post-form .sound-input",

            function (e) {

                e.preventDefault();
                var form = $(this).closest('form');


                if (!this.files[0]) {
                    ClearMediaFileAndPreview("soundFile", form);
                    return;
                }
                if (this.files[0].size > Common.sound_Max_File_Size) {
                     
                    swal('Your file is too big');
                    ClearMediaFileAndPreview("soundFile", form);
                    return;
                }

                ClearMediaFiles("soundFile", form);
                SetMediaPreview("soundFile", form);
                SetMediaPosition($(this));

                var audio_player = $(form).find('audio').get(0);
                var fileURL = URL.createObjectURL(this.files[0]);
                audio_player.src = fileURL;
                audio_player.play();
            });
    } catch (e) {
        console.log(e);
    }
};
function VideoPreview           ($this) {
    try {

        var videoLink = $this.closest('.videoLink');
        var iframeDiv = $(videoLink).find('.h_iframe-aparat_embed_frame').get(0);
        var alert = $(videoLink).find('.alert').get(0);

        var linkUrl = $this.val();
        linkUrl = NormalizeVideoURL(linkUrl);
        var searchPattern = new RegExp('^https://www.aparat.com/v/', 'i');
        var isOK = searchPattern.test(linkUrl);

        if (!isEmptyOrSpaces(linkUrl) && isOK) {

            $(iframeDiv).show();
            $(alert).hide();

            var iframe = $(videoLink).find('iframe').get(0);
            var linkParts = linkUrl.split('/');
            var linkId = linkParts[linkParts.length - 1];
            iframe.src = 'https://www.aparat.com/video/video/embed/videohash/' + linkId + '/vt/frame';

        } else {
            $(iframeDiv).hide();
            $(alert).show();
        }
    } catch (e) {
        console.log(e);
    }
};
function VideoLinkChange        () {

    $(".posts").on(
        "input",
        ".post-card .post-form .post-media .videoLink .videoLinkText",

        function () {
            VideoPreview($(this));
        });

    $(".posts").on(
        "input propertychange paste",
        ".post-card .post-form .post-media .videoLink .videoLinkText",
        function () {
            VideoPreview($(this));
        });
    
};

function ImageAttachClick       (e) {
    try {
        e.preventDefault();

        var form = $(this).closest('form'); 
        var imageInput = $(form).find('input#PictureFile2').get(0);
        $(imageInput).click();
    } catch (e) {
        console.log(e);
    }
};
function SoundAttachClick       (e) {
    try {
        e.preventDefault();
        var form = $(this).closest('form');
        var soundInput = $(form).find('input[name="SoundFile"]').get(0);
        $(soundInput).click();
    } catch (e) {
        console.log(e);
    }
};
function VideoAttachClick       (e) {
    try {
        e.preventDefault();
        var form = $(this).closest('form');

        ClearMediaFiles("videoLink", form);
        SetMediaPreview("videoLink", form);
        SetMediaPosition($(this));
    } catch (e) {
        console.log(e);
        
    }
};

function ResetForm              () {

    try {
        $('.post-form').bind('reset', function () {

            var form = $(this);

            ClearMediaFiles("All", form);
            ClearMediaFileAndPreview("All", form);
            var contentInput = $(form).find('.content-editor').get(0);
            var contentQuill = Quill.find(contentInput);
            contentQuill.setText('');

        });
    } catch (e) {
        console.log(e);
    }
};
function ShowForm               () {
    try {

    } catch (e) {
        console.log(e);
    }
};

function InitEditor             (form) {
    var toolbarOptions =
        [
            ['bold', 'italic', 'underline'],
            ['blockquote', 'link'],
        ];

    var contentInput = $(form).find('input[name="Content"]').get(0);
    var container = $(form).find('.content-editor').get(0);

    var quill = new Quill(container,
        {
            placeholder: 'Post Content',
            theme: 'bubble',
            modules: {
                toolbar: toolbarOptions
            }
        });

    quill.on('text-change', function (eventName, args) {

        $(contentInput).val(quill.root.innerHTML); 
    });

    $('.ql-bubble').addClass('ql-direction-rtl');
    $('.ql-editor').addClass('ql-direction-rtl');

};

function ResetpostsContainer() {
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
function SetuppostsLoading() {
    try {
        infinite = new Waypoint.Infinite({
            element: $('.posts')[0],
            items: '.post-item',
            more: '.next-page',
            onAfterPageLoad: function (e) {
                var player = $(e).find('.audio-player');
                const players = Array.from(player).map(p => new Plyr(p));
            }
        });
    } catch (e) {
        console.log(e);
    }

};
