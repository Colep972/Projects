document.addEventListener("DOMContentLoaded", () => {
    const btn = document.getElementById('pdfBtn');
    if (!btn) return;

    btn.addEventListener('click', () => {
        const { jsPDF } = window.jspdf;
        const doc = new jsPDF('p', 'mm', 'a4');

        doc.setFontSize(14);
        doc.text("Journal alimentaire", 14, 15);

        // Récupération des totaux
        const totauxDiv = document.getElementById("totaux-data");
        const kcal = totauxDiv.dataset.kcal;
        const glucides = totauxDiv.dataset.glucides;
        const lipides = totauxDiv.dataset.lipides;
        const proteines = totauxDiv.dataset.proteines;

        // On ajoute la ligne Totaux directement dans le tableau HTML
        const table = document.querySelector('.Journal tbody');
        if (table) {
            const tr = document.createElement('tr');

            const cellules = ["Totaux", kcal, glucides, lipides, proteines, "-", "-"];
            cellules.forEach(text => {
                const td = document.createElement('td');
                td.textContent = text;
                td.style.fontWeight = 'bold'; // <-- Totaux en gras
                tr.appendChild(td);
            });

            table.appendChild(tr); // ajoute la ligne Totaux
        }

        // Génération du PDF
        doc.autoTable({
            html: '.Journal',
            startY: 20,
            styles: { fontSize: 10, cellPadding: 3 },
            headStyles: { fillColor: [41, 128, 185], textColor: [255, 255, 255] },
            theme: 'grid',
        });

        doc.save('repas-du-jour.pdf');

        // Optionnel : retirer la ligne Totaux du HTML après génération
        if (table && table.lastElementChild.textContent.startsWith("Totaux")) {
            table.removeChild(table.lastElementChild);
        }
    });
});
