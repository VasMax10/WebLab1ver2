const uri = 'api/CastsAPI/';
const ActorsUri = 'api/ActorsAPI/';
const CharactersUri = 'api/CharactersAPI/';
let casts = [];
let characters = [];
let actors = [];

function getCasts() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayCasts(data))
        .catch(error => console.error('Unable to get casts.', error));
}
function getActors() {
    fetch(ActorsUri)
        .then(response => response.json())
        .then(data => _displayActors(data))
        .catch(error => console.error('Unable to get actors.', error));
}

function getCharacters() {
    fetch(CharactersUri)
        .then(response => response.json())
        .then(data => _displayCharacters(data))
        .catch(error => console.error('Unable to get characters.', error));
}

function _displayCasts(data) {
    const list = document.getElementById('list');

    list.innerHTML = '';
    data.forEach(cast => {

        list.innerHTML += '<div class="flip-card card mb-3" style="display: inline-block; margin:10px">' +
            '<div class="flip-card-inner">' +
            '<div class="flip-card-front">' +
        '<img class="card-img-top" src="' + cast.actorPhoto + '" alt="Card image cap">' +
        '<div class="card-body">' +
        '<h5 class="card-title">' + cast.actorName + '</h5>' +
        '</div>' +
        '</div>' +
        '<div class="flip-card-back">' +
            '<img class="card-img-top flip-img" src="' + cast.characterPhoto + '" alt="Card image cap">'  +
        '<div class="card-body" style="padding-top:0px">' +
            '<h5 class="card-title" style="margin-bottom:0px">as ' + cast.characterName + '</h5>' +
            '<h6 class="card-title">Seasons (' + cast.firstAppereance + ' - ' + cast.lastAppereance + ')</h6>' +
        '<a class="btn btn-secondary text-light" style="margin-left:10px" onclick="showEditMenu(' + cast.id + ')">Edit</a>' +
        '<a class="btn btn-danger text-light" style="margin-left:10px" onclick="showDeleteMenu('+ cast.id +')">Delete</a>' + 
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>';
    });


    casts = data;
}

function _displayActors(data) {
    var combo = document.getElementById('actors');
    var combo_edit = document.getElementById('actors-edit');
    data.forEach(actor => {
        var option = document.createElement('option');
        option.appendChild(document.createTextNode(actor.name));
        combo.appendChild(option);

        var option_edit = document.createElement('option');
        option_edit.appendChild(document.createTextNode(actor.name));
        combo_edit.appendChild(option_edit);
    });
    actors = data;
}

function _displayCharacters(data) {
    var combo = document.getElementById('characters');
    var combo_edit = document.getElementById('characters-edit');
    data.forEach(character => {
        var option = document.createElement('option');
        option.appendChild(document.createTextNode(character.name));
        combo.appendChild(option);

        var option_edit = document.createElement('option');
        option_edit.appendChild(document.createTextNode(character.name));
        combo_edit.appendChild(option_edit);
    });
    characters = data;
}

function closeInput() {
    document.getElementById('addCast').style.display = 'none';
    document.getElementById('changeCast').style.display = 'none';
    document.getElementById('deleteCast').style.display = 'none';
}

function showAddMenu() {
    document.getElementById('addCast').style.display = 'block';
    modal = document.getElementById("addCast");
}

function addCast() {
    const Character = document.getElementById('characters').value;
    const Actor = document.getElementById('actors').value;
    const cast =
    {
        actorID: actors.find(actor => actor.name === Actor).id,
        characterID: characters.find(character => character.name === Character).id,
        firstAppereance: Number(document.getElementById('add-first-ap').value),
        lastAppereance: Number(document.getElementById('add-last-ap').value)
    };
    fetch(uri, {
        method: 'POST',
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