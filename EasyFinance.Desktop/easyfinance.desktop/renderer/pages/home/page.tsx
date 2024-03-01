import HeaderComponent from "../../components/layout/headerComponent/headerComponent"
import TabsComponent from "../../components/layout/tabsComponent/tabsComponent";
import FooterComponent from "../../components/layout/footerComponent/footerComponent"


const Home = () => {

    return (
        <div className="parent">
            <HeaderComponent />
            <TabsComponent />
            <FooterComponent />
        </div>

    )
};

export default Home;