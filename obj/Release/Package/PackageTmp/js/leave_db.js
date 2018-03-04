(function() {

    var db = {

        loadData: function(filter) {
            return $.grep(this.clients, function(client) {
                return (!filter.Name || client.Name.indexOf(filter.Name) > -1)
                    && (filter.Age === undefined || client.Age === filter.Age)
                    && (!filter.Address || client.Address.indexOf(filter.Address) > -1)
                    && (!filter.Country || client.Country === filter.Country)
                    && (filter.Married === undefined || client.Married === filter.Married);
            });
        },

        insertItem: function(insertingClient) {
            this.clients.push(insertingClient);
        },

        updateItem: function(updatingClient) { },

        deleteItem: function(deletingClient) {
            var clientIndex = $.inArray(deletingClient, this.clients);
            this.clients.splice(clientIndex, 1);
        }

    };

    window.db = db;


   

    db.clients = [
        {
            "Leave Request Id": "LR-0002",
            "Leave Type": "30 Days",
            "Request Date": "7-2-2016",
            "Personnal Number": "000128",
            "worker":"Ahmed mahmoud zidan",
            "From Date":"12-12-2016",
            "To Date":"12-10-2016"
        },
        {
            "Leave Request Id": "LR-0002",
            "Leave Type": "30 Days",
            "Request Date": "7-2-2016",
            "Personnal Number": "000128",
            "worker":"Ahmed mahmoud zidan",
            "From Date":"12-12-2016",
            "To Date":"12-10-2016"
        },
        {
            "Leave Request Id": "LR-0002",
            "Leave Type": "30 Days",
            "Request Date": "7-2-2016",
            "Personnal Number": "000128",
            "worker":"Ahmed mahmoud zidan",
            "From Date":"12-12-2016",
            "To Date":"12-10-2016"
        },
        {
            "Leave Request Id": "LR-0002",
            "Leave Type": "30 Days",
            "Request Date": "7-2-2016",
            "Personnal Number": "000128",
            "worker":"Ahmed mahmoud zidan",
            "From Date":"12-12-2016",
            "To Date":"12-10-2016"
        },
       {
            "Leave Request Id": "LR-0002",
            "Leave Type": "30 Days",
            "Request Date": "7-2-2016",
            "Personnal Number": "000128",
            "worker":"Ahmed mahmoud zidan",
            "From Date":"12-12-2016",
            "To Date":"12-10-2016"
        },
        {
            "Leave Request Id": "LR-0002",
            "Leave Type": "30 Days",
            "Request Date": "7-2-2016",
            "Personnal Number": "000128",
            "worker":"Ahmed mahmoud zidan",
            "From Date":"12-12-2016",
            "To Date":"12-10-2016"
        },
        {
            "Leave Request Id": "LR-0002",
            "Leave Type": "30 Days",
            "Request Date": "7-2-2016",
            "Personnal Number": "000128",
            "worker":"Ahmed mahmoud zidan",
            "From Date":"12-12-2016",
            "To Date":"12-10-2016"
        },
        {
            "Leave Request Id": "LR-0002",
            "Leave Type": "30 Days",
            "Request Date": "7-2-2016",
            "Personnal Number": "000128",
            "worker":"Ahmed mahmoud zidan",
            "From Date":"12-12-2016",
            "To Date":"12-10-2016"
        },
        {
            "Leave Request Id": "LR-0002",
            "Leave Type": "30 Days",
            "Request Date": "7-2-2016",
            "Personnal Number": "000128",
            "worker":"Ahmed mahmoud zidan",
            "From Date":"12-12-2016",
            "To Date":"12-10-2016"
        },
       
       
    ];

   

}());