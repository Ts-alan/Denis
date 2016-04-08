$(function(){           
        if (!Modernizr.inputtypes.date) {
        // If not native HTML5 support, fallback to jQuery datePicker
            $('input[type=date]').datepicker({
                // Consistent format with the HTML5 picker
                    dateFormat : 'yy-mm-dd'
                },
                // Localization
                $.datepicker.regional['it']
            );
        }
    });