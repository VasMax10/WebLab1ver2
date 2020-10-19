const uri = 'api/SeriesAPI/';
let series = [];

function getSeries() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displaySeries(data))
        .catch(error => console.error('Unable to get series.', error));
}

function _displaySeries(data) {
    const list = document.getElementById('list');

    list.innerHTML = '';
    data.forEach(series => {
        var status = 'In progress';
        if (series.isEnded === true) 
            status = 'Ended';
        list.innerHTML +=   '<div class="card" style="margin-left:30px">' +
                                '<div class="img-container">' +
                                    '<img src="' + series.poster +'" alt="" />' +
                                '</div>' +
                                '<div class="card-details">' +
                                    '<div style="text-align:end">' +
                                        '<a  class="btn btn-secondary mini-btn">Edit</a>' +
                                        '<a  class="btn btn-danger mini-btn" style="margin-right:-15px">Delete</a>' +
                                    '</div>' +
                                    '<h2 style="margin-top:-30px">' + series.name + '('+ series.premiere +')</h2>' +
                                    '<h5>Status: ' + status + '</h5>'  +
                                    '<p>' +series.info + '</p>' +
                                    '<br />' +
                                    '<div style="text-align: center; position:fixed; bottom:10px;">' +
                                        '<a class="btn btn-info" style="width:105px">Cast</a>' +
                                        '<a class="btn btn-info" style="width:145px">Characters</a>' + 
                                    '</div>' +
                                '</div>' +
                            '</div>';
    });


    series = data;
}

function closeInput() {
    document.getElementById('addCast').style.display = 'none';
    document.getElementById('changeCast').style.display = 'none';
    document.getElementById('deleteCast').style.display = 'none';
}

function showAddMenu() {
    document.getElementById('addSeries').style.display = 'block';
    modal = document.getElementById("addSeries");
}

function addSeries() {
    const series =
    {
        title: document.getElementById('add-title').value,
        poster: document.getElementById('add-poster').value,
        firstAired: Number(document.getElementById('add-aired').value),
        isEnded: document.getElementById('add-is-ended').value,
        backImg: document.getElementById('add-back-img').value,
        mainColor: document.getElementById('add-main-color').value,
        secondColor: document.getElementById('add-second-color').value,
        info: document.getElementById('add-info').value,
    };
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(series)
    })
        .then(() => {
            getSeries();
    })
        .then(response => response.json())
        .catch(error => console.error('Unable to add series', error));
    closeInput();
}

function changeCast() {
    const ID = document.getElementById('change-id').value;
    const Character = document.getElementById('characters-edit').value;
    const Actor = document.getElementById('actors-edit').value;
    const cast =
    {
        id: Number(ID),
        actorID: actors.find(actor => actor.name === Actor).id,
        characterID: characters.find(character => character.name === Character).id,
        firstAppereance: Number(document.getElementById('change-first-ap').value),
        lastAppereance: Number(document.getElementById('change-last-ap').value)
    };
    fetch(`${uri}${ID}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(cast)
    })
        .then(() => {
            getCasts();
            document.getElementById('characters').value = '';
            document.getElementById('actors').value = '';
        })
        .then(response => response.json())
        .catch(error => console.error('Unable to add cast', error));
    closeInput();


    closeInput();
}

function showEditMenu(id) {
    document.getElementById('changeCast').style.display = 'block';

    modal = document.getElementById('changeCast');
    document.getElementById('change-id').value = id;
    const cast = casts.find(cast => cast.id === id);
    document.getElementById('change-first-ap').value = cast.firstAppereance;
    document.getElementById('change-last-ap').value = cast.lastAppereance;

    const actor = actors.find(actor => actor.id === cast.actorID);
    document.getElementById('actors-edit').value = actor.name;

    const character = characters.find(character => character.id === cast.characterID);
    document.getElementById('characters-edit').value = character.name;
}

function showDeleteMenu(id) {
    document.getElementById('deleteCast').style.display = 'block';
    document.getElementById('delete-form').setAttribute('onclick', `deleteCast(${id})`);
}

function deleteCast(id) {
    fetch(`${uri}${id}`, {
        method: 'DELETE'
    })
        .then(() => getCasts())
        .then(() => closeInput())
        .catch(error => console.error('Unable to delete cast', error));
}

window.onclick = function (event) {

    if (event.target == modal) {
        modal.style.display = "none";
    }
}