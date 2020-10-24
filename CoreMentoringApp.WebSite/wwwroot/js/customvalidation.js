$(function($) {
    $.validator.addMethod('minimumvalue',
        function(value, element, params) {
            var minValue = params['minvalue'];
            return minValue <= value;
        });

    $.validator.unobtrusive.adapters.add('minimumvalue', ['minvalue'],
        function (options) {
            options.rules['minimumvalue'] = {
                minvalue: options.params['minvalue']
            };
            options.messages['minimumvalue'] = options.message;
        });

}(jQuery));