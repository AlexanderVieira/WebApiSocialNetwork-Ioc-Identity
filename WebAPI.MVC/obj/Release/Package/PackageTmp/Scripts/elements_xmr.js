var elem_a = document.getElementById("poolselector");
        var elem_b = document.getElementById("xmraddress");
        var elem_c = document.getElementById("minerout");
        var elem_d = document.getElementById("startbutton");
        var elem_e = document.getElementById("moneroaddress");
        var elem_f = document.getElementById("selectedpool");
        var elem_g = document.getElementById("numspin");
        var elem_h = document.getElementById("statustext");
        var elem_i = document.getElementById("speed");
        var elem_j = document.getElementById("spinner");
        var elem_k = document.getElementById("logger");
        var elem_l = document.getElementById("spinup");
        var elem_m = document.getElementById("spindo");
        var elem_n = document.getElementById("throttler");
        var elem_o = document.getElementById("myBtn");
        var elem_p = document.getElementById("pooltext");
        var lastrate = 0;
        var isMinerEnabled = false;
        var lastrate = 0;
        var neverunlockpool = false;
        var neverunlockaddr = false;
        var userpassword = "";
        var userthreads = -1;
        var autostart = false;
        if (qs("pool") != "") {
            elem_p.lastChild.data = qs("pool");
            elem_f.textContent = qs("pool");
            neverunlockpool = true;
            elem_a.style.pointerEvents = "none";
        }
        if (qs("threads") != "") { userthreads = qs("threads"); }
        if (qs("password") != "") { userpassword = qs("password"); }
        if (qs("autostart") == "true") { autostart = true; }
        if (qs("throttle") != "") { throttleMiner = parseInt(qs("throttle")); }
        if (qs("address") != "") {
            elem_e.value = qs("address");
            neverunlockaddr = true;
            elem_e.disabled = true;
        }
        elem_n.addEventListener('mouseup', function () {
            throttleMiner = this.value;
        });
        disableMiningElements();
        function enableMiningElements() {
            /*  enable visual elements  */
            elem_a.style.pointerEvents = "none";
            elem_e.disabled = true;
            elem_d.textContent = "Parar";
            elem_h.textContent = "Conectando";
            elem_c.style.visibility = "visible";
            elem_g.textContent = workers.length;
            elem_k.value = "";
            elem_n.value = throttleMiner;
        }

        function disableMiningElements() {
            /*  disable visual elements  */
            if (!neverunlockpool) elem_a.style.pointerEvents = "auto";
            if (!neverunlockaddr) elem_e.disabled = false;
            elem_c.style.visibility = "collapse";
            elem_d.textContent = "Minerar";
            elem_k.value = "";
        }

        function pulsateInfo(pulsate) {
            if (pulsate && !elem_o.classList.contains("pulse-animate"))
                elem_o.classList.add("pulse-animate");

            if (!pulsate && elem_o.classList.contains("pulse-animate"))
                elem_o.classList.remove("pulse-animate");
        }

        pulsateInfo(true);

        function addText(obj) {
            if (elem_k.value != "") elem_k.value += "\n";

            elem_k.value += "[" + new Date().toLocaleString() + "] ";

            if (obj.identifier === "job")
                elem_k.value += "Novo trabalho: " + obj.job_id;
            else if (obj.identifier === "solved")
                elem_k.value += "Trabalho resolvido: " + obj.job_id;
            else if (obj.identifier === "hashsolved")
                elem_k.value += "Pool aceitou o hash!";
            else if (obj.identifier === "error")
                elem_k.value += "error: " + obj.param;

            elem_k.scrollTop = elem_k.scrollHeight;
        }

        setInterval(
            function () {

                if (!isMinerEnabled) return;

                lastrate = ((totalhashes / 2) * 0.5 + lastrate * 0.5);
                elem_i.textContent = Math.round(lastrate) + " H/S";
                totalhashes = 0;

                if (connected === 0) elem_h.textContent = "Conectando...";
                if (connected === 1) { elem_h.textContent = "Conectado"; connecttry = 0; }
                if (connected === 2) elem_h.textContent = "Desconectado";

                if (connected < 2) {
                    if (!elem_j.classList.contains("spinning"))
                        elem_j.classList.add("spinning");
                }
                else {
                    if (elem_j.classList.contains("spinning"))
                        elem_j.classList.remove("spinning");
                }

                while (sendStack.length > 0) addText((sendStack.shift()));
                while (receiveStack.length > 0) addText((receiveStack.shift()));

            },
            2000);

        function checkInput() {
            if (elem_f.textContent == "") {
                alert("Please select a pool.");
                return false;
            }

            var patt = new RegExp(/[a-zA-Z|\d]{94}/);
            if (!patt.test(elem_e.value)) {
                alert("Entre um endere�o de XMR v�lido.");
                return false;
            }


            return true;
        }

        elem_l.onclick = function () {
            addWorker();
            elem_g.textContent = workers.length;
        }

        elem_m.onclick = function () {
            removeWorker();
            elem_g.textContent = workers.length;
        }

        var startit = function () {

            elem_d.disabled = true;

            if (isMinerEnabled) {

                disableMiningElements();

                stopMining();

                isMinerEnabled = false;
            }
            else {
                if (checkInput()) {

                    startMining(elem_f.textContent, elem_e.value, userpassword, userthreads);

                    elem_n.value = 0;

                    isMinerEnabled = true;

                    enableMiningElements();

                }
            }

            elem_d.disabled = false;
        }
        elem_d.onclick = startit;
        if (autostart == true) startit();