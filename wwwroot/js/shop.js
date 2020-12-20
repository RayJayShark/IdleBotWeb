function Confirm(playerId, itemId, itemCost) {
    const button = document.getElementById(itemId);
    button.innerText = "Sure?";
    button.setAttribute("onclick", `Buy('${playerId}', ${itemId}, ${itemCost})`);
}

function Buy(playerId, itemId, itemCost) {
    axios.post(`/Game/Shop/${playerId}?itemId=${itemId}&itemCost=${itemCost}`)
        .then(res => {
            const money = document.getElementById("money");
            money.innerText -= itemCost;
            const button = document.getElementById(itemId);
            button.innerText = "Buy";
            button.setAttribute("onclick", `Confirm('${playerId}', ${itemId}, ${itemCost})`);
        });
    ;
}