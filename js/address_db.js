(function() {

    var db = {

        loadData: function(filter) {
            return $.grep(this.addresses, function(client) {
                return (!filter.Name || client.Name.indexOf(filter.Name) > -1)
                    && (filter.Age === undefined || client.Age === filter.Age)
                    && (!filter.Address || client.Address.indexOf(filter.Address) > -1)
                    && (!filter.Country || client.Country === filter.Country)
                    && (filter.Married === undefined || client.Married === filter.Married);
            });
        },

        insertItem: function(insertingAddress) {
            this.addresses.push(insertingAddress);
        },

        updateItem: function(updatingAddress) { },

        deleteItem: function(deletingAddress) {
            var clientIndex = $.inArray(deletingAddress, this.addresses);
            this.addresses.splice(clientIndex, 1);
        }

    };

    window.db = db;


   

    db.addresses = [
        {
            "ID": "1",
            "Country": "Eygpt",
            "Address": "12-albaida streat",
        },
        {
            "ID": "2",
            "Country": "Eygpt",
            "Address": "12-albaida streat",
        },
        
       
    ];

   

}());