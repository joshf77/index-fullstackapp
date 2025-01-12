import { createRouter, createWebHistory} from 'vue-router'
import HomePage from './../components/HomePage.vue'
import EventsPage from '../components/EventsPage.vue'
import AboutUs from '../components/AboutUs.vue'
import NewsPage from '../components/NewsPage.vue'
import Staff from '../components/Staff.vue'
import Event from '../components/Event.vue'
import News from '../components/News.vue'
import Contact from '../components/Contact.vue'


const routes = [
    {
        path: '/',
        name: 'HomePage',
        component: HomePage
    },
    {
        path: '/events',
        name: 'EventsPage',
        component: EventsPage
    },
    {
        path: '/event/:id',
        name: 'Event',
        component: Event
    },
    {
        path: '/news',
        name: 'Newsletter',
        component: NewsPage
    },
    {
        path: '/newsletter/:id',
        name: 'news',
        component: News
    },
    {
        path: '/about',
        name: 'AboutUs',
        component: AboutUs
    },
    {
        path: '/contact',
        name: 'Contact',
        component: Contact
    },
    {
        path: '/staff/:id',
        name: 'staff',
        component: Staff
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
  })

export default router