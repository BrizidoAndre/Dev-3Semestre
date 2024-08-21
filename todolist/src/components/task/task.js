import { useId, useState } from 'react';
import { LuPencil } from "react-icons/lu";
import { FaTrash } from "react-icons/fa";
import { ImCheckboxChecked, ImCheckboxUnchecked } from "react-icons/im";

import './task.css'
import { ModalCreate } from '../modalCreate/modalCreate';
import { ModalAlter } from '../modalAlter/modalAlter';

export const Task = ({ index, list, setList, item }) => {
    // id a ser usado para o input e a label
    const inputId = useId()

    //modal de alteração da task selecionada
    const [modalAlter, setModalAlter] = useState(false)

    // alterar apenas os status da task e então atualiza a lista com o novo item
    function changeStatus() {
        let index = list.findIndex(task => task.id === item.id)
        list[index] = {
            id: item.id,
            name: item.name,
            status: !item.status
        }

        console.log(list)

        setList([
            ...list
        ])
    }

    // deletando a task identificada dentro da lista de tasks
    function deleteTask() {
        let newList = list.filter(task => task.id !== item.id);

        setList([
            ...newList
        ])
    }

    return (
        <>
            {// o modal de alteração mostra apenas quando o state para ele for true
                modalAlter ?
                    <ModalAlter item={item} list={list} setList={setList} setModal={setModalAlter} />
                    : null
            }
            <div className={item.status ? 'container-task checked' : 'container-task'}>
                <div className='container-task-input' onClick={changeStatus}>
                    {
                        //com base nos status mostrar o icone correto
                        // se for true, mostra a tarefa como realizada senão mostra como a fazer
                        item.status ?
                            <ImCheckboxChecked size={20} />
                            :
                            <ImCheckboxUnchecked size={20} style={{minWidth:'20'}} />
                    }
                    <label htmlFor={inputId}>
                        {item.name}
                    </label>
                </div>
                <div className='container-actions'>
                    <button onClick={deleteTask}>
                        <FaTrash />
                    </button>
                    {/* ao clicar mostrar o modal de alteração */}
                    <button onClick={() => { setModalAlter(true) }}>
                        <LuPencil />
                    </button>
                </div>
            </div>
        </>
    )
}