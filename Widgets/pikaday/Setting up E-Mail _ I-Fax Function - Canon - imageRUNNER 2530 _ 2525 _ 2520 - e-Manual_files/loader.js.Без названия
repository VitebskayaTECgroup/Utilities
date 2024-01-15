try {
	var head = document.getElementsByTagName('head')[0];

	// resource.jsonのロード
	var o = eval(constant);

	// アセットモードの判定
	var asset_mode = o[0]['asset_mode'];

	// フレームパスの取得
	var frame_path = o[0]['frame_path'];

	// テンプレートパスの取得
	var template_path = o[0]['template_path'];

	// search.json
	var search_mode = o[0]['search_mode'];
	if (search_mode != 'gcs') { // ローカル検索モードの場合のみ必要
		var script = document.createElement('script');
		script.type = 'text/javascript';
		script.charset = 'utf-8';
		script.src = '../jsons/search.json';
		head.appendChild(script);
	}

	strWindowType = 'MAIN';

	// 共通アセットをロード
	// --------------------------------------------------------------------------------------------
	if (asset_mode == 'web') {

		// frame_style.css
		var link = document.createElement('link');
		link.rel = 'stylesheet';
		link.type = 'text/css';
		link.href = frame_path + "styles/frame_style.css";
		head.appendChild(link);

		// 目次モードの判定
		var toc_mode = o[0]['toc_mode'];
		if (toc_mode == 'json') {

			// frame_toc.css
			var link = document.createElement('link');
			link.rel = 'stylesheet';
			link.type = 'text/css';
			link.href = frame_path + 'styles/frame_toc_json.css';
			head.appendChild(link);
		}

		// style.css
		var link = document.createElement('link');
		link.rel = 'stylesheet';
		link.type = 'text/css';
		link.href = template_path + 'style.css';
		head.appendChild(link);

		// jquery.js
		var script = document.createElement('script');
		script.type = 'text/javascript';
		script.charset = 'utf-8';
		script.src = frame_path + 'scripts/jquery.js';
		head.appendChild(script);

		// library.js
		var script = document.createElement('script');
		script.type = 'text/javascript';
		script.charset = 'utf-8';
		script.src = frame_path + 'scripts/library.js';
		head.appendChild(script);

		// script.js
		window.setTimeout(function() {
			var script = document.createElement('script');
			script.type = 'text/javascript';
			script.charset = 'utf-8';
			script.src = frame_path + 'scripts/script.js';
			head.appendChild(script);
		}, 500); // 先にjquery.js, library.jsが読み込まれることを防ぐ

		// google analytics
		var ga_tracking_id = o[0]['ga_tracking_id'];
		var ga_tracking_domain = o[0]['ga_tracking_domain'];
		if (ga_tracking_id) {
			(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
				(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
				m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
			})(window,document,'script','//www.google-analytics.com/analytics.js','ga');
			ga('create', ga_tracking_id, ga_tracking_domain);
			ga('send', 'pageview');
		}

	// 配下にあるアセットをロード
	// --------------------------------------------------------------------------------------------
	} else {

		// フレームパスを直下にセット
		var frame_path = '../';

		// テンプレートパスを直下にセット
		var template_path = '../contents/styles/style000/';

		// frame_style.css
		var link = document.createElement('link');
		link.rel = 'stylesheet';
		link.type = 'text/css';
		link.href = frame_path + "styles/frame_style.css";
		head.appendChild(link);

		// 目次モードの判定
		var toc_mode = o[0]['toc_mode'];
		if (toc_mode == 'json') {

			// frame_toc.css
			var link = document.createElement('link');
			link.rel = 'stylesheet';
			link.type = 'text/css';
			link.href = frame_path + 'styles/frame_toc_json.css';
			head.appendChild(link);
		}

		// style.css
		var link = document.createElement('link');
		link.rel = 'stylesheet';
		link.type = 'text/css';
		link.href = template_path + 'style.css';
		head.appendChild(link);

		// jquery.js
		var script = document.createElement('script');
		script.type = 'text/javascript';
		script.charset = 'utf-8';
		script.src = frame_path + 'scripts/jquery.js';
		head.appendChild(script);

		// library.js
		var script = document.createElement('script');
		script.type = 'text/javascript';
		script.charset = 'utf-8';
		script.src = frame_path + 'scripts/library.js';
		head.appendChild(script);

		// script.js
		var script = document.createElement('script');
		script.type = 'text/javascript';
		script.charset = 'utf-8';
		script.src = frame_path + 'scripts/script.js';
		head.appendChild(script);
	}

	// 電話番号認識無効化（onloadではタイミングが遅い）
	// <meta name="format-detection" content="telephone=no" />
	var meta = document.createElement('meta');
	meta.setAttribute('name', 'format-detection');
	meta.setAttribute('content', 'telephone=no');
	head.appendChild(meta);

} catch (e) {
}