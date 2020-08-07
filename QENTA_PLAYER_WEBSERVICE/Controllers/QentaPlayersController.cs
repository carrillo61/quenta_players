using QENTA_PLAYER_DATA;
using QENTA_PLAYER_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QENTA_PLAYER_WEBSERVICE.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QentaPlayersController : ApiController
    {
        private DALC_Players dALC_Players = new DALC_Players();

        // GET: api/Players
        public IEnumerable<E_players> Get()
        {
            return dALC_Players.GetPlayers();
        }

        // GET: api/Players/5
        public E_players Get(int id)
        {
            return dALC_Players.GetPlayer(id);
        }

        // POST: api/Players
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Players/5
        public IHttpActionResult Put(int id, E_players player)
        {
            if(id > 0 && player != null)
            {
                E_players playerUpdate = dALC_Players.UpdatePlayer(player);

                return Ok(playerUpdate);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        // DELETE: api/Players/5
        public void Delete(int id)
        {
        }
    }
}
