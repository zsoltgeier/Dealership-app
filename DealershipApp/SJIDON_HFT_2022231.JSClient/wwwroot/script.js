let cars = [];
let connection = null;


let carIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:12969/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CarCreated", (user, message) => {
        getdata();
    });

    connection.on("CarDeleted", (user, message) => {
        getdata();
    });

    connection.on("CarUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


fetch('http://localhost:12969/car')
    .then(x => x.json())
    .then(y => {
        cars = y;
        console.log(cars);
        display();
    });

async function getdata() {
    await fetch('http://localhost:12969/car')
        .then(x => x.json())
        .then(y => {
            cars = y;
            //console.log(passangers);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    cars.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.model + "</td><td>"
            + t.horsepower + "</td><td>"
            + t.price + "</td><td>"
            + t.brand_Id + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}





function create() {
    let model = document.getElementById('model').value;
    let horsepower = document.getElementById('horsepower').value;
    let price = document.getElementById('price').value;
    let brandid = document.getElementById('brandid').value;
    fetch('http://localhost:12969/car', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { model: model, horsepower: horsepower, price: price,brand_Id: brandid})
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function remove(id) {
    fetch('http://localhost:12969/car/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let model = document.getElementById('modeltoupdate').value;
    let horsepower = document.getElementById('horsepowertoupdate').value;
    let price = document.getElementById('pricetoupdate').value;
    let brandid = document.getElementById('brandidtoupdate').value;
    fetch('http://localhost:12969/car', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { model: model, horsepower: horsepower, price: price, brand_Id: brandid, id: carIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function showupdate(id) {
    document.getElementById('modeltoupdate').value = cars.find(t => t['id'] == id)['model'];
    document.getElementById('horsepowertoupdate').value = cars.find(t => t['id'] == id)['horsepower'];
    document.getElementById('pricetoupdate').value = cars.find(t => t['id'] == id)['price'];
    document.getElementById('brandidtoupdate').value = cars.find(t => t['id'] == id)['brand_Id'];
    document.getElementById('updateformdiv').style.display = 'flex';
    carIdToUpdate = id;
}

function displayQueryResult1() {
    fetch('http://localhost:12969/stat/GetCarWhereMoreThan18Employees')
        .then(response => response.json())
        .then(data => {
            let result = document.getElementById('queryresult1');
            result.innerHTML = ""; // clear existing content
            data.forEach(item => {
                result.innerHTML += "<p>" + JSON.stringify(item) + "</p>";
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function displayQueryResult2() {
    fetch('http://localhost:12969/stat/GetCarWhereBrandOwnerIsBMWGroup')
        .then(response => response.json())
        .then(data => {
            let result = document.getElementById('queryresult2');
            result.innerHTML = ""; // clear existing content
            data.forEach(item => {
                result.innerHTML += "<p>" + JSON.stringify(item) + "</p>";
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function displayQueryResult3() {
    fetch('http://localhost:12969/stat/GetDealershipWhereCar313hp')
        .then(response => response.json())
        .then(data => {
            let result = document.getElementById('queryresult3');
            result.innerHTML = ""; // clear existing content
            data.forEach(item => {
                result.innerHTML += "<p>" + JSON.stringify(item) + "</p>";
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function displayQueryResult4() {
    fetch('http://localhost:12969/stat/GetDealershipWhereCarModelIsCharger')
        .then(response => response.json())
        .then(data => {
            let result = document.getElementById('queryresult4');
            result.innerHTML = ""; // clear existing content
            data.forEach(item => {
                result.innerHTML += "<p>" + JSON.stringify(item) + "</p>";
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function displayQueryResult5() {
    fetch('http://localhost:12969/stat/GetDealershipWherePriceIs209700')
        .then(response => response.json())
        .then(data => {
            let result = document.getElementById('queryresult5');
            result.innerHTML = ""; // clear existing content
            data.forEach(item => {
                result.innerHTML += "<p>" + JSON.stringify(item) + "</p>";
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
}