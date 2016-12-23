/* 
 * select.js 
 * This is only for demonstration purposes and only changes
 * a dropdown toggle's label without fireing an event!!!
 */

(function($) {

  'use strict'; // jshint ;_;

  $.fn.dropSelect = function(option) {
    return this.each(function() {

      var $this = $(this);
      var display = $this.find('.dropdown-toggle');        
      var options = $this.find('ul.dropdown-menu > li > a');

      var onOptionClick = function(event) {
        event.preventDefault();
        display.html($(this).html() + ' <span class="caret"></span>');
      };

      options.click(onOptionClick);

    });
  };

  $(function() {
    $('div[data-select=true]').dropSelect();
  });

})(window.jQuery);