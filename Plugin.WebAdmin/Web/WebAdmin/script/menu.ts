﻿namespace VRS.WebAdmin
{
    import ViewJson = VirtualRadar.Plugin.WebAdmin;

    export class Menu
    {
        private _MenuEntries: ViewJson.IJsonMenuEntry[] = [];
        private _MenuItemsList: JQuery;

        constructor()
        {
            $(document).ready(() => {
                this.addTopNavbar();
                this.addNavSidebar();

                $('[data-toggle=offcanvas]').click(function() {
                    $('.row-offcanvas').toggleClass('active');
                });

                this.fetchMenuEntries();
            });
        }

        private fetchMenuEntries()
        {
            $.ajax({
                url: 'ViewMap.json',
                success: (menuEntries: ViewJson.IJsonMenuEntry[]) => {
                    this._MenuEntries = menuEntries;
                    this.populateMenu();
                },
                error: () => {
                    setTimeout(() => this.fetchMenuEntries, 5000);
                }
            });
        }

        /**
         * Adds the HTML for the top navigation bar.
         */
        private addTopNavbar()
        {
            var html =
                $('<div />')
                .attr('class', 'navbar navbar-default navbar-fixed-top" role="navigation')
                .append($('<div />').attr('class', 'container-fluid')
                    .append($('<div />').attr('class', 'navbar-header')
                        .append($('<button />')
                            .attr('type', 'button')
                            .attr('class', 'navbar-toggle')
                            .attr('data-toggle', 'offcanvas')
                            .attr('data-target', '.sidebar-nav')
                            .attr('aria-label', 'Menu')
                            .append($('<span />').addClass('icon-bar'))
                            .append($('<span />').addClass('icon-bar'))
                            .append($('<span />').addClass('icon-bar'))
                        )
                        .append($('<a />')
                            .attr('class', 'navbar-brand')
                            .attr('href', '#')
                            .text(VRS.WebAdmin.$$.WA_Title_WebAdmin)
                        )
                    )
                );

            $('#page-container').prepend(html);
        }

        /**
         * Adds the HTML for the sidebar.
         */
        private addNavSidebar()
        {
            var sidebar = $('<nav />')
                .attr('id', 'sidebar')
                .attr('role', 'navigation')
                .addClass('col-xs-6 col-sm-3 sidebar-offcanvas hidden-print');
            this._MenuItemsList = $('<ul />').addClass('nav').appendTo(sidebar);

            $('#content > .row').prepend(sidebar);
        }

        /**
         * Adds the menu items to the list.
         */
        private populateMenu()
        {
            var currentPageUrl = location.pathname.substring(location.pathname.lastIndexOf('/') + 1);

            this._MenuItemsList.empty();
            $.each(this._MenuEntries, (idx, page) => {
                var isCurrentPage = VRS.stringUtility.equals(currentPageUrl, page.HtmlFileName, true);
                var pageElement = $('<li />');
                if(isCurrentPage) {
                    pageElement.text(page.Name).addClass('active');
                } else {
                    pageElement.append($('<a />').attr('href', page.HtmlFileName).text(page.Name));
                }
                this._MenuItemsList.append(pageElement);
            });
        }
    }

    export var menu = new Menu();
} 