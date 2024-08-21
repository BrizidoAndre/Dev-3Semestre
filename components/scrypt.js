
        // para realizar a animação de accordeão
        let acc = document.getElementsByClassName('accordion')

        for (let i = 0; i < acc.length; i++) {
            
            acc[i].addEventListener('click',function(){
                let panel = this.nextElementSibling;
                panel.classList.toggle('fixed-height')
            })
        }



        // para realizar a navegação de tab
        let tabs = document.getElementsByClassName('nav-tab')

        let tabsContent = document.getElementsByClassName('tab-content')


        for (let index = 0; index < tabs.length; index++) {
            const tab = tabs[index];
            
            tab.addEventListener('click', function(){
                
                for (let index = 0; index < tabsContent.length; index++) {
                    
                    tabs[index].classList.remove('active')
                    tabsContent[index].classList.remove('tab-content-show')
                }

                tab.classList.add('active')
                tabsContent[index].classList.add('tab-content-show')
            })
        }



        // para realizar a o banner

        