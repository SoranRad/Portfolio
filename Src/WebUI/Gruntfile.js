module.exports = function (grunt) {
    grunt.initConfig({
        clean: ["wwwroot/dist/css/*", "wwwroot/dist/js/*"],

        sass: {
            dist: {
                options: {
                    style: 'expanded'
                },
                files: {
                    "wwwroot/lib/_custom/css/bootstrap-ex.css":
                    "wwwroot/lib/_custom/scss/style.scss"
                }
            }
        },

        uglify:
        {
            js:
            {
                src: [
                    'wwwroot/lib/jquery/jquery.js',
                    'wwwroot/lib/jquery-migrate/jquery-migrate.js',
                    'wwwroot/lib/jquery-ajax-unobtrusive-file-upload/jquery.unobtrusive-ajax-Ex.js',

                    'wwwroot/lib/popper.js/umd/popper.js',
                    'wwwroot/lib/bootstrap/dist/js/bootstrap.bundle.js',
                     
                    'wwwroot/lib/jquery-validation/dist/jquery.validate.min.js',
                    'wwwroot/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js',

                    'wwwroot/lib/lazysizes/lazysizes.js',
                    'wwwroot/lib/sweetalert/sweetalert.min.js',
                    'wwwroot/lib/tippy.js/tippy.umd.js',
                    'wwwroot/lib/waypoints/jquery.waypoints.js',
                    'wwwroot/lib/waypoints/shortcuts/infinite.js',   
                    'wwwroot/lib/plyr/plyr.js',
                    'wwwroot/lib/toastr.js/toastr.min.js',
                    'wwwroot/lib/jquery.ns-autogrow/jquery.ns-autogrow.js', 
                    'wwwroot/lib/lightbox2/js/lightbox.js', 


                    'wwwroot/lib/_custom/js/ajax-helper.js',
                    'wwwroot/lib/_custom/js/articles.js',
                    'wwwroot/lib/_custom/js/blog.js',
                    'wwwroot/lib/_custom/js/charity.js',
                    'wwwroot/lib/_custom/js/charityList.js',
                    'wwwroot/lib/_custom/js/login.js',
                    'wwwroot/lib/_custom/js/signup.js',
                    'wwwroot/lib/_custom/js/users.js',
                    'wwwroot/lib/_custom/js/push-web.js',
                    'wwwroot/lib/_custom/js/activation.js',
                    'wwwroot/lib/_custom/js/CharityGallery.js',
                    'wwwroot/lib/_custom/js/Follow.js',
                    'wwwroot/lib/_custom/js/Suggestion.js',
                    'wwwroot/lib/_custom/js/Comment.js',
                    'wwwroot/lib/_custom/js/share_modal.js',
                    'wwwroot/lib/_custom/js/TopCharity.js',
                    'wwwroot/lib/_custom/js/Category.js',

                    'wwwroot/lib/_custom/js/common.js',
                    'wwwroot/lib/_custom/js/main.js'

    ],
                dest: 'wwwroot/dist/js/site.min.js'
            }
        },
        cssmin: {
            options: {
                mergeIntoShorthands: false,
                roundingPrecision: -1
            },
            target: {
                files: {
                    'wwwroot/dist/css/site.min.css':
                    [
                        'wwwroot/lib/font-awesome/css/all.css',
                        'wwwroot/lib/_custom/css/rtl/nz-bootstrap.css',
                        'wwwroot/lib/quill/quill.bubble-Ex.css',
                        'wwwroot/lib/tippy.js/tippy.css',
                        'wwwroot/lib/flickity/flickity.css',
                        'wwwroot/lib/leaflet/leaflet.css',
                        'wwwroot/lib/jquery-nice-select/css/nice-select-Ex.css',
                        'wwwroot/lib/plyr/plyr.css',
                        'wwwroot/lib/toastr.js/toastr.css',
                        'wwwroot/lib/lightbox2/css/lightbox.css',
                        'wwwroot/lib/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar-ex.css',

                        'wwwroot/lib/_custom/fonts/font.css',

                        'wwwroot/lib/_custom/css/articles.css',
                        'wwwroot/lib/_custom/css/helper.css',
                        'wwwroot/lib/_custom/css/login.css',
                        'wwwroot/lib/_custom/css/signup.css',
                        'wwwroot/lib/_custom/css/blog.css',
                        'wwwroot/lib/_custom/css/charity.css',
                        'wwwroot/lib/_custom/css/Gallery.css',
                        'wwwroot/lib/_custom/css/users.css',
                        'wwwroot/lib/_custom/css/header.css',
                        'wwwroot/lib/_custom/css/sidebar.css',
                        'wwwroot/lib/_custom/css/footer.css',
                        'wwwroot/lib/_custom/css/content.css' ,
                        'wwwroot/lib/_custom/css/Category.css',
                        'wwwroot/lib/_custom/css/services.css',
                        'wwwroot/lib/_custom/css/nikokar.css'

                    ]
                }
            }
        } 
       
    });

    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks("grunt-contrib-uglify-es");
    grunt.loadNpmTasks('grunt-contrib-cssmin'); 
};