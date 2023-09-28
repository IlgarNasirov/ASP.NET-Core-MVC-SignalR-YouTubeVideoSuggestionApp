var suggestionConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/suggestion").build();

getAllSuggestions = () => {
    fetch("/Suggestion/GetAllSuggestions")
        .then(response => response.json())
        .then(data => {
            const table = document.createElement('table');
            table.classList.add('table', 'table-hover', 'table-secondary');

            const thead = document.createElement('thead');

            const headerRow = document.createElement('tr');

            const userNameHeader = document.createElement('th');
            userNameHeader.textContent = "Username";
            headerRow.appendChild(userNameHeader);

            const shortDescriptionHeader = document.createElement('th');
            shortDescriptionHeader.textContent = "Short description";
            headerRow.appendChild(shortDescriptionHeader);

            const youTubeLinkHeader = document.createElement('th');
            youTubeLinkHeader.textContent = "YouTube link";
            headerRow.appendChild(youTubeLinkHeader);

            const updateSuggestionHeader = document.createElement('th');
            updateSuggestionHeader.textContent = "Update suggestion";
            headerRow.appendChild(updateSuggestionHeader);

            const deleteSuggestionHeader = document.createElement('th');
            deleteSuggestionHeader.textContent = "Delete suggestion";
            headerRow.appendChild(deleteSuggestionHeader);
            thead.appendChild(headerRow);
            table.appendChild(thead);

            const tbody = document.createElement('tbody');

            data.forEach(item => {

                const bodyRow = document.createElement('tr');

                const userNameData = document.createElement('td');
                userNameData.textContent = item.username;
                bodyRow.appendChild(userNameData);

                const shortDescriptionData = document.createElement('td');
                shortDescriptionData.textContent = item.shortDescription;
                bodyRow.appendChild(shortDescriptionData);

                const youTubeLinkData = document.createElement('td');

                const linkYouTubeVideoButton = document.createElement('a');
                linkYouTubeVideoButton.href = item.youTubeUrl;
                linkYouTubeVideoButton.classList.add('btn', 'btn-success');
                linkYouTubeVideoButton.textContent = "Link";
                youTubeLinkData.appendChild(linkYouTubeVideoButton);
                bodyRow.appendChild(youTubeLinkData);

                const updateSuggestionData = document.createElement('td');

                const updateSuggestionButton = document.createElement('a');
                updateSuggestionButton.href = `/Suggestion/UpdateSuggestion?id=${item.id}`;
                updateSuggestionButton.classList.add('btn', 'btn-warning');
                updateSuggestionButton.textContent = "Edit";
                updateSuggestionData.appendChild(updateSuggestionButton);
                bodyRow.appendChild(updateSuggestionData);

                const deleteSuggestionData = document.createElement('td');

                const deleteSuggestionButton = document.createElement('a');
                deleteSuggestionButton.href = `/Suggestion/DeleteSuggestion?id=${item.id}`;
                deleteSuggestionButton.classList.add('btn', 'btn-danger');
                deleteSuggestionButton.textContent = "Delete";
                deleteSuggestionData.appendChild(deleteSuggestionButton);
                bodyRow.appendChild(deleteSuggestionData);
                tbody.appendChild(bodyRow);
            });
            table.appendChild(tbody);

            const div = document.getElementsByClassName('table-responsive')[0];
            div.innerHTML = '';
            div.appendChild(table);
            const loading = document.getElementById('loading');
            if (loading != null) {
                loading.style.display = "none";
            }
        })
        .catch(() => console.log("Something went wrong!"));
}

const alertSuccess = document.getElementsByClassName('alert-success')[0];
if (alertSuccess != null) {
    setTimeout(() => {
        alertSuccess.style.display = "none";
    }, 5000);
}

document.addEventListener("DOMContentLoaded", () => getAllSuggestions());

suggestionConnection.on("suggestionAdded", () => getAllSuggestions());

suggestionConnection.on("suggestionUpdated", () => getAllSuggestions());

suggestionConnection.on("suggestionDeleted", () => getAllSuggestions());

suggestionConnection.start();