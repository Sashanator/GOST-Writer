/* eslint-disable jsx-a11y/anchor-is-valid */
import React from "react";
import './Header.scss';

export const Header: React.FC = () => {
    return (
        <nav>
		<ul>
			<li><a href="#">О нас</a></li>
			<li><a href="#">Сервисы</a>
				<ul>
					<li><a href="#">Антиплагиат</a></li>
					<li><a href="#">Авто ГОСТ</a>
						<ul>
							<li><a href="#">Ручная проверка</a></li>
						</ul>
					</li>
					<li><a href="#">Помощь с написанием кусовой</a></li>
				</ul>
			</li>
			<li><a href="#">Цены</a></li>
		</ul>
	</nav>
    );
}